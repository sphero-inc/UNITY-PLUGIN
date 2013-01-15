using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System.Collections.Generic;

#if UNITY_IPHONE

public class SpheroProviderIOS : SpheroProvider {
	
	/*
	 * Get the Robot Provider for Android 
	 */
	public SpheroProviderIOS() : base() {
		m_PairedSpheros = new Sphero[1];
		// DO NOT CHANGE THIS UNTIL MULTIPLE ROBOTS ARE CREATED
		m_PairedSpheros[0].DeviceInfo.UniqueId = "iOSRobot";
	}
	
	/*
	 * Call to properly disconnect Spheros.  Call in OnApplicationPause 
	 */
	override public void DisconnectSpheros() {
		//
	}
	
	/* Need to call this to get the robot objects that are paired from Android */
	override public bool FindRobots() {
		return false;
	}
	
	/* Check if bluetooth is on */
	override public bool IsAdapterEnabled() {
		return false;	
	}
	
	/* Connect to a robot at index */
	override public void Connect(int index) {
		// Don't try to connect to multiple Spheros at once
		if( GetConnectingSphero() != null ) return;
	}	
}

#endif
