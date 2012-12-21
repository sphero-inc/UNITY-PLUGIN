using UnityEngine;
using System.Collections;


public class ConnectionGUI : MonoBehaviour {
	
	/*
	 * Set the number of buttons that will be the Spheros to pair in the future 
	 */
	public GUISkin m_SpheroConnectionSkin;
	
	
	// Loading image
	public Texture2D m_Spinner;
	public Vector2 m_SpinnerSize = new Vector2(128, 128);
	public float m_SpinnerAngle = 0;
	Vector2 m_SpinnerPosition = new Vector2(0, 0);
	Vector2 m_SpinnerPivotPos = new Vector2(0, 0);
	Rect m_SpinnerRect;
	
	// UI Padding Variables
	int m_ViewPadding = 20;
	int m_ElementPadding = 10;
	
	// Button Size Variables
	int m_ButtonWidth = 200;
	int m_ButtonHeight = 50;
	
	// Title Variables
	int m_TitleWidth = 120;
	int m_TitleHeight = 40;
	string m_Title = "Connect to a Sphero";
	public GUIStyle m_TitleStyle = new GUIStyle();
	
	// Sphero Name Label Variable
	int m_SpheroLabelWidth = 200;
	int m_SpheroLabelHeight = 100;
	int m_SpheroLabelSelected = -1;
	
	// Native Java Objects
	AndroidJavaObject m_RobotProvider;
	AndroidJavaObject m_PairedRobots;

	// Paired Sphero Info
	int m_PairedRobotCount;
	string[] m_RobotNames;
	int m_RobotConnectingIndex = -1;
	
	// Use this for initialization
	void Start () {
		
		// Initialize GUI 
		
		
		// The SDK uses alot of handlers that need a valid Looper in the thread, so set that up here
        using (AndroidJavaClass jc = new AndroidJavaClass("android.os.Looper"))
        {
        	jc.CallStatic("prepare");
        }
		
		using (AndroidJavaClass jc = new AndroidJavaClass("orbotix.robot.base.RobotProvider"))
        {
			// Grab a handle on the RobotProvider
            m_RobotProvider = jc.CallStatic<AndroidJavaObject>("getDefaultProvider");
			bool isAdapterEnabled = m_RobotProvider.Call<bool>("isAdapterEnabled");   
			
			// Only run this stuff if the adapter is enabled
			if( isAdapterEnabled ) {
				m_RobotProvider.Call("findRobots");  
				m_PairedRobots = m_RobotProvider.Call<AndroidJavaObject>("getRobots");
				m_PairedRobotCount = m_PairedRobots.Call<int>("size");
				
				// Store the robots that are paired into an array
				m_RobotNames = new string[m_PairedRobotCount];
				for( int i = 0; i < m_PairedRobotCount; i++ ) {
					AndroidJavaObject jo = m_PairedRobots.Call<AndroidJavaObject>("get", i);	
					m_RobotNames[i] = jo.Call<string>("getName");
				}
			}
        }
		
		// For debugging UI
//		m_RobotNames = new string[4];
//		m_RobotNames[0] = "Hello-0";
//		m_RobotNames[1] = "Hello-1";
//		m_RobotNames[2] = "Hello-2";
//		m_RobotNames[3] = "Hello-3";
	}
	
	void OnApplicationPause() {
		using (AndroidJavaClass jc = new AndroidJavaClass("orbotix.robot.base.RobotProvider"))
		{
	        m_RobotProvider = jc.CallStatic<AndroidJavaObject>("getDefaultProvider");
			m_RobotProvider.Call("disconnectControlledRobots");	
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	// Called when the GUI should update
	void OnGUI() {
		
		GUI.skin = m_SpheroConnectionSkin;
		
		// Draw a title lable
		GUI.Label(new Rect(m_ViewPadding,m_ViewPadding,Screen.width-(m_ViewPadding*2),m_TitleHeight), m_Title, "label");
		
		// Set up the scroll view that holds all the Sphero names
		int scrollY = m_ViewPadding + m_TitleHeight + m_ElementPadding;
		int scrollHeight = Screen.height-(m_ViewPadding*2)-m_TitleHeight-m_ButtonHeight-(m_ElementPadding*2);
		Vector2 scrollPosition = Vector2.zero;	
		scrollPosition = GUI.BeginScrollView (
                      new Rect (m_ViewPadding,scrollY,Screen.width-(m_ViewPadding*2),scrollHeight),  // screen position
                      scrollPosition,             												     // current scroll position
                      new Rect (0, 0, m_SpheroLabelWidth, m_PairedRobotCount*m_SpheroLabelHeight)    // content area
                 );
		
		// Show a grid of Spheros to connect to
		m_SpheroLabelSelected = GUI.SelectionGrid(new Rect(0,0,m_SpheroLabelWidth,m_SpheroLabelHeight),m_SpheroLabelSelected,m_RobotNames,1,"toggle");
		GUI.EndScrollView();
		
		// Set up the Connect Button
		//GUI.color = new Color(0.11f,0.56f,1f,1f);
		int connectBtnX = (Screen.width/2)-(m_ButtonWidth/2);
		int connectBtnY = Screen.height-m_ViewPadding-m_ButtonHeight;
		if( GUI.Button(new Rect(connectBtnX,connectBtnY,m_ButtonWidth,m_ButtonHeight), "Connect" )) {
		
			// Check if we have a Sphero connected
			if( m_SpheroLabelSelected >= 0 ) {
				// Grab a handle on the RobotProvider
				using (AndroidJavaClass jc = new AndroidJavaClass("orbotix.robot.base.RobotProvider"))
				{
	        		m_RobotProvider = jc.CallStatic<AndroidJavaObject>("getDefaultProvider");
					m_RobotProvider.Call("control", m_SpheroLabelSelected);
					m_RobotProvider.Call<AndroidJavaObject>("connectControlledRobots");
				}
			
				// Adjust title info
				m_RobotConnectingIndex = m_SpheroLabelSelected;
				m_Title = "Connecting to " + m_RobotNames[m_SpheroLabelSelected];
			}
		}
		
		// Only show the connection dialog if we are connecting to a robot
		if( m_RobotConnectingIndex >= 0 ) {
			GUI.Box(m_SpinnerRect,"");
			
			m_SpinnerPosition.x = Screen.width/2;
			m_SpinnerPosition.y = Screen.height/2;
			// Rotate the object
			m_SpinnerRect = new Rect(m_SpinnerPosition.x - m_SpinnerSize.x * 0.5f, m_SpinnerPosition.y - m_SpinnerSize.y * 0.5f, m_SpinnerSize.x, m_SpinnerSize.y);
        	m_SpinnerPivotPos = new Vector2(m_SpinnerRect.xMin + m_SpinnerRect.width * 0.5f, m_SpinnerRect.yMin + m_SpinnerRect.height * 0.5f);
			
			// Draw the new image
	        Matrix4x4 matrixBackup = GUI.matrix;
	        GUIUtility.RotateAroundPivot(m_SpinnerAngle, m_SpinnerPivotPos);
	        GUI.DrawTexture(m_SpinnerRect, m_Spinner);
	        GUI.matrix = matrixBackup;
			m_SpinnerAngle+=3;
		}
	}
}
