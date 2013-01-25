# HelloWorld Example

---
The HelloWorld example demonstrates how to properly manage a Sphero application lifecycle and send basic commands to the ball to change its color.


## Sphero Application Lifecycle

---

This subject is important, because you need to make sure Sphero is always in a stable state during and after your app executes.  To accomplish this, you need to be properly shutting down everything you register for, including Sphero itself.  The code below shows the way to properly register/unregister notification delegates, show the `NoSpheroConnectionScene`, and disconnect Sphero.

	void ViewSetup() {
		// Get Connected Sphero
		m_Spheros = SpheroProvider.GetSharedProvider().GetConnectedSpheros();
		SpheroDeviceMessenger.SharedInstance.NotificationReceived += ReceiveNotificationMessage;
		if( m_Spheros.Length == 0 ) Application.LoadLevel("SpheroConnectionScene");
	}
	
	void Start () {	
		ViewSetup();
	}
	
	void OnApplicationPause(bool pause) {
		if( pause ) {
			// Initialize the device messenger which sets up the callback
			SpheroDeviceMessenger.SharedInstance.NotificationReceived -= ReceiveNotificationMessage;
			SpheroProvider.GetSharedProvider().DisconnectSpheros();
		}
		else {
			ViewSetup();
		}
	}
	
If your app is only a Sphero app, it is wise to check if any Spheros are connected at the start, so you can then bring up the connection scene (especially if the user brings the app back up from the background).

The OnApplicationPause() method is called on Android and iOS when the user presses the home button.  At this point, our app is going into the background, so we call the SpheroProvider function to disconnect any connected Spheros.  This will properly disconnect them and automatically put them in a stable state. 

## Blinking Sphero Blue

You can change the color of Sphero, along with accessing it's current color, by using the code below.  The color change only occurs at a frequency of once every 20 Update() calls.

		m_BlinkCounter++;
		if( m_BlinkCounter % 20 == 0 ) {			
			foreach( Sphero sphero in m_Spheros ) {
				// Set the Sphero color to blue 
				if( sphero.RGBLEDColor.Equals(BLACK) ) {
					sphero.SetRGBLED(BLUE.r,BLUE.g,BLUE.b);
				}
				else {
					sphero.SetRGBLED(BLACK.r,BLACK.g,BLACK.b);	
				}
			}
		}
		
The current color of the individual Sphero is set when the user calls the SetRGBLED() function.
		
## Dealing with Sphero Disconnects

Sphero can disconnect for a few reasons.  A few instances are if the battery dies, it travels out of the bluetooth range, or the user places it in the charger.  If any of these occur, the Unity Plugin will send a notification to all registered classes notifying of a disconnect.  In this example, we load the `NoSpheroConnectedScene` if a user's ball disconnects.

	private void ReceiveNotificationMessage(object sender, SpheroDeviceMessenger.MessengerEventArgs eventArgs)
	{
		SpheroDeviceNotification message = (SpheroDeviceNotification)eventArgs.Message;
		Sphero notifiedSphero = SpheroProvider.GetSharedProvider().GetSphero(message.RobotID);
		if( message.NotificationType == SpheroDeviceNotification.SpheroNotificationType.DISCONNECTED ) {
			notifiedSphero.ConnectionState = Sphero.Connection_State.Disconnected;
			Application.LoadLevel("NoSpheroConnectedScene");
		}
	}
		

## Community and Help

---

* [Developer Forum](http://forum.gosphero.com/) - Share your project, get help and talk to the Sphero developers!