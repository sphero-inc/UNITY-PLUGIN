using UnityEngine;
using System.Collections;

//Gui joystick overlay built specifically for Spherov2

public class Joystick : MonoBehaviour {
	
	struct Boundary {
		public Vector2 min;
		public Vector2 max;
	};
	
	public Vector2 deadZone = Vector2.zero;
	public bool normalize = false;
	public Vector2 position;

	private int lastFingerId = -1;								// Finger last used for this joystick
	private Vector2 fingerDownPos;
	private float fingerDownTime;
	private float firstDeltaTime = 0.5f;

	private GUITexture gui;								// Joystick graphic
	private Rect defaultRect;								// Default position / extents of the joystick graphic
	private Boundary guiBoundary;			// Boundary for joystick graphic
	private Vector2 guiTouchOffset;						// Offset to apply to touch input
	private Vector2 guiCenter;							// Center of joystick

	private float velocityScale = 0.6f;
	private bool stopped = true;
	private float lastHeading = 0.0f;
	
	// Use this for initialization
	void Start () {
		// Cache this component at startup instßead of looking up every frame	
		gui = GetComponent<GUITexture>();
		
		

		// Store the default rect for the gui, so we can snap back to it
		defaultRect = gui.pixelInset;	
		
		defaultRect.x += transform.position.x * Screen.width;
		defaultRect.y += transform.position.y * Screen.height;
		
		//this.transform.position.x = 0.0f;
		//this.transform.position.y = 0.0f;
		transform.position = new Vector3(0.0f, 0.0f, 0.0f);
		
					
		// This is an offset for touch input to match with the top left
		// corner of the GUI
		guiTouchOffset.x = defaultRect.width * 0.5f;
		guiTouchOffset.y = defaultRect.height * 0.5f;
		
		// Cache the center of the GUI, since it doesn't change
		guiCenter.x = defaultRect.x + guiTouchOffset.x;
		guiCenter.y = defaultRect.y + guiTouchOffset.y;
		
		// Let's build the GUI boundary, so we can clamp joystick movement
		guiBoundary.min.x = defaultRect.x - guiTouchOffset.x;
		guiBoundary.max.x = defaultRect.x + guiTouchOffset.x;
		guiBoundary.min.y = defaultRect.y - guiTouchOffset.y;
		guiBoundary.max.y = defaultRect.y + guiTouchOffset.y;
	}
	
	void Disable()
	{
		gameObject.active = false;
	}
	
	void ResetJoystick()
	{
		// Release the finger control and set the joystick back to the default position
		gui.pixelInset = defaultRect;
		lastFingerId = -1;
		position = Vector2.zero;
		fingerDownPos = Vector2.zero;
			
	}
	
	bool IsFingerDown()
	{
		return (lastFingerId != -1);
	}
		
	void LatchedFinger(int fingerId)
	{
		// If another joystick has latched this finger, then we must release it
		if ( lastFingerId == fingerId )
			ResetJoystick();
	}
	
	// Update is called once per frame
	void Update () {
			
		int count = Input.touchCount;
		
		
		if ( count == 0 )
			ResetJoystick();
		else
		{
			for(int i =0; i < count; i++)
			{
				Touch touch = Input.GetTouch(i);			
				Vector2 guiTouchPos = touch.position - guiTouchOffset;
		
				bool shouldLatchFinger = false;
				if ( gui.HitTest( touch.position ) )
				{
					shouldLatchFinger = true;
				}		
		
				// Latch the finger if this is a new touch
				if ( shouldLatchFinger && ( lastFingerId == -1 || lastFingerId != touch.fingerId ) )
				{
					lastFingerId = touch.fingerId;					
				}				
		
				if ( lastFingerId == touch.fingerId )
				{	
								
					// Change the location of the joystick graphic to match where the touch is
					Vector2 clampedPosition;
					clampedPosition.x =  Mathf.Clamp( guiTouchPos.x/* - (gui.pixelInset.width * 0.25f)*/, guiBoundary.min.x, guiBoundary.max.x );
					clampedPosition.y =  Mathf.Clamp( guiTouchPos.y/* - (gui.pixelInset.height * 0.25f)*/, guiBoundary.min.y, guiBoundary.max.y );

					gui.pixelInset = new Rect(clampedPosition.x, clampedPosition.y, gui.pixelInset.width, gui.pixelInset.height);
					
					if ( touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled )
						ResetJoystick();					
				}			
			}
		}
		

		// Get a value between -1 and 1 based on the joystick graphic location
		position.x = ( gui.pixelInset.x + guiTouchOffset.x - guiCenter.x ) / guiTouchOffset.x;
		position.y = ( gui.pixelInset.y + guiTouchOffset.y - guiCenter.y ) / guiTouchOffset.y;

		// Adjust for dead zone	
		var absoluteX = Mathf.Abs( position.x );
		var absoluteY = Mathf.Abs( position.y );
		
		if ( absoluteX < deadZone.x )
		{
			// Report the joystick as being at the center if it is within the dead zone
			position.x = 0;
		}
		else if ( normalize )
		{
			// Rescale the output after taking the dead zone into account
			position.x = Mathf.Sign( position.x ) * ( absoluteX - deadZone.x ) / ( 1 - deadZone.x );
		}
			
		if ( absoluteY < deadZone.y )
		{
			// Report the joystick as being at the center if it is within the dead zone
			position.y = 0;
		}
		else if ( normalize )
		{
			// Rescale the output after taking the dead zone into account
			position.y = Mathf.Sign( position.y ) * ( absoluteY - deadZone.y ) / ( 1 - deadZone.y );
		}
		
		Vector2 inputVector = position;
		float heading = Mathf.Atan2(inputVector.y, 0.0f - inputVector.x);
		float velocity = inputVector.magnitude;
		
		//Commented out for weird Sphero v2 calibration
		heading -= Mathf.PI / 2.0f;
		if ( heading < 0.0f )
		{
			heading += 2.0f * Mathf.PI;
		}
		
		float degrees = Mathf.Rad2Deg * heading;
		
		//Include the heading offset for auto-heading adjust
		/*var headingDiff : float = RKUNBridge.s_calibrateGyroOffset - Input.gyro.attitude.eulerAngles.z;
		degrees += headingDiff + RKUNBridge.s_calibrateOffset;*/
		
		while(degrees < 0.0f) {
			degrees += 360.0f;
		}
		while(degrees > 359.0f) {
			degrees -= 360.0f;
		}
		
	#if !UNITY_EDITOR
		velocity = velocity * velocityScale;
		if(velocity > velocityScale) {
			velocity = velocityScale;
		}
		if(velocity > 0.0f) {
			RKUNBridge._RKUNRoll((int)degrees, velocity);
			stopped = false;
			lastHeading = degrees;
		} else if(velocity == 0.0f && stopped == false) {
			stopped = true;
			RKUNBridge._RKUNRoll((int)lastHeading, 0.0f);
		}
	#endif
		
	}
	

}
