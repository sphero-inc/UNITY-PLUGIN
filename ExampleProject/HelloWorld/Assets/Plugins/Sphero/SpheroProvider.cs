using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System.Collections.Generic;

public abstract class SpheroProvider {
	
	/* Shared instance of Sphero Provider */
	static SpheroProvider sharedProvider = null;
	
	/* Robots */
	protected Sphero[] m_PairedSpheros;
	public Sphero[] PairedSpheros {
		get{ return this.m_PairedSpheros; }	
	}
	
	/*
	 * Default Constructor
	 */
	public SpheroProvider() {
		// Initialize the device messenger which sets up the callback
		SpheroDeviceMessenger.SharedInstance.NotificationReceived += ReceiveNotificationMessage;
	}
	
	/* Get the shared RobotProvider instance */
	public static SpheroProvider GetSharedProvider() {
		if( sharedProvider == null ) {
			#if UNITY_ANDROID			
				sharedProvider = new SpheroProviderAndroid();
			#else
				sharedProvider = new SpheroProviderIOS();
			#endif			
		}
		return sharedProvider;
	}
	
	/*
	 * Callback to receive connection notifications 
	 */
	private void ReceiveNotificationMessage(object sender, SpheroDeviceMessenger.MessengerEventArgs eventArgs)
	{
		SpheroDeviceNotification message = (SpheroDeviceNotification)eventArgs.Message;
		Sphero notifiedSphero = GetSphero(message.RobotID);
		if( message.NotificationType == SpheroDeviceNotification.SpheroNotificationType.CONNECTED ) {
			notifiedSphero.ConnectionState = Sphero.Connection_State.Connected;
			Debug.Log ("Connected Notification Processed");
		}
		else if( message.NotificationType == SpheroDeviceNotification.SpheroNotificationType.DISCONNECTED ) {
			notifiedSphero.ConnectionState = Sphero.Connection_State.Disconnected;
		}
		else if( message.NotificationType == SpheroDeviceNotification.SpheroNotificationType.CONNECTION_FAILED ) {
			notifiedSphero.ConnectionState = Sphero.Connection_State.Failed;
		}
	}
	
	/*
	 * Get a Sphero object from the unique Sphero id 
	 * Returns nulls if no Spheros were found with that particular id
	 */
	public Sphero GetSphero(string spheroId) {
		foreach( Sphero sphero in m_PairedSpheros ) {
			if( sphero.DeviceInfo.UniqueId.Equals(spheroId)) {
				return sphero;	
			}
		}
		return null; 
	}

	/* Grab the connecting Robot */
	public Sphero GetConnectingSphero() {
		foreach( Sphero sphero in m_PairedSpheros ) {
			if( sphero.ConnectionState == Sphero.Connection_State.Connecting ) {
				return sphero;
			}	
		}
		return null;
	}
	
	/* Grab the robot names from Java array */
	public string[] GetRobotNames() {
		// Store the robots that are paired into an array
		string[] robotNames = new string[m_PairedSpheros.Length];	
		for( int i = 0; i < m_PairedSpheros.Length; i++ ) {
			robotNames[i] = m_PairedSpheros[i].GetName();
		}		
		return robotNames;
	}
	
	/*
	 * Get the robots that are currently connected
	 */  
	public List<Sphero> GetConnectedSpheros() {
		List<Sphero> connectedSpheros = new List<Sphero>();
		foreach( Sphero sphero in m_PairedSpheros ) {
			if( sphero.ConnectionState == Sphero.Connection_State.Connected ) {
				connectedSpheros.Add(sphero);	
			}
		}	
		return connectedSpheros;
	}
	
	
	/*
	 * Call to properly disconnect Spheros.  Call in OnApplicationPause 
	 */
	abstract public void DisconnectSpheros();
	/* Need to call this to get the robot objects that are paired from Android */
	abstract public bool FindRobots();
	/* Check if bluetooth is on */
	abstract public bool IsAdapterEnabled();
	/* Connect to a robot at index */
	abstract public void Connect(int index);	
}
