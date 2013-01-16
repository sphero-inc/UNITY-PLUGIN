using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HelloWorld : MonoBehaviour {
	
	Color BLUE = new Color(0,0,1.0f,1.0f);
	Color BLACK = new Color(0,0,0,1.0f);
	
	/* Connected Sphero Robot */
	List<Sphero> m_SpheroList;
	
	/* Counter to determine if Sphero should have color or not */
	int m_BlinkCounter;
	
	/* Use this for initialization */
	void ViewSetup() {
		// Get Connected Sphero
		m_SpheroList = SpheroProvider.GetSharedProvider().GetConnectedSpheros();
		SpheroDeviceMessenger.SharedInstance.NotificationReceived += ReceiveNotificationMessage;
		if( m_SpheroList.Count == 0 ) Application.LoadLevel("SpheroConnectionScene");
	}
	
	void Start () {	
		ViewSetup();
	}
	
	/* This is called when the application returns from or enters background */
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
	
	/* Update is called once per frame */
	void Update () {
		m_BlinkCounter++;
		if( m_BlinkCounter % 20 == 0 ) {			
			foreach( Sphero sphero in m_SpheroList ) {
				// Set the Sphero color to blue 
				if( sphero.RGBLEDColor.Equals(BLACK) ) {
					sphero.SetRGBLED(BLUE.r,BLUE.g,BLUE.b);
				}
				else {
					sphero.SetRGBLED(BLACK.r,BLACK.g,BLACK.b);	
				}
			}
		}
	}

	/*
	 * Callback to receive connection notifications 
	 */
	private void ReceiveNotificationMessage(object sender, SpheroDeviceMessenger.MessengerEventArgs eventArgs)
	{
		SpheroDeviceNotification message = (SpheroDeviceNotification)eventArgs.Message;
		if( message.NotificationType == SpheroDeviceNotification.SpheroNotificationType.DISCONNECTED ) {
			m_SpheroList[0].ConnectionState = Sphero.Connection_State.Disconnected;
			Application.LoadLevel("NoSpheroConnectedScene");
		}
	}
}