using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UISample : MonoBehaviour {
	
	Color BLUE = new Color(0,0,1.0f,1.0f);
	
	/* Connected Sphero Robot */
	Sphero[] m_Spheros;
	
	/* Use this for initialization */
	void ViewSetup() {
		// Get Connected Spheros
		m_Spheros = SpheroProvider.GetSharedProvider().GetConnectedSpheros();
		SpheroDeviceMessenger.SharedInstance.NotificationReceived += ReceiveNotificationMessage;
		if( m_Spheros.Length == 0 ) Application.LoadLevel("SpheroConnectionScene");
		foreach( Sphero sphero in m_Spheros ) {
			sphero.SetRGBLED(BLUE.r,BLUE.g,BLUE.b);
		}
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
	void Update () {}

	/*
	 * Callback to receive connection notifications 
	 */
	private void ReceiveNotificationMessage(object sender, SpheroDeviceMessenger.MessengerEventArgs eventArgs)
	{
		SpheroDeviceNotification message = (SpheroDeviceNotification)eventArgs.Message;
		Sphero notifiedSphero = SpheroProvider.GetSharedProvider().GetSphero(message.RobotID);
		if( message.NotificationType == SpheroDeviceNotification.SpheroNotificationType.DISCONNECTED ) {
			notifiedSphero.ConnectionState = Sphero.Connection_State.Disconnected;
			m_Spheros = SpheroProvider.GetSharedProvider().GetConnectedSpheros();
			if( m_Spheros.Length == 0) Application.LoadLevel("NoSpheroConnectedScene");
		}
	}
}