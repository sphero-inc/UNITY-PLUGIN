using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class SpheroProvider {
	
	// Shared instance of Sphero Provider
	static SpheroProvider sharedProvider = null;
	
#if UNITY_ANDROID	
	Sphero[] m_Spheros;
#endif
	
	public SpheroProvider() {}
	
	public static SpheroProvider getSharedProvider() {
		if( sharedProvider == null ) {
			sharedProvider = new SpheroProvider();	
		}
		return sharedProvider;
	}
	
	/*
	 * Get the robots that were connected by the Sphero Connection Scene 
	 */  
	public Sphero[] getConnectedSpheros() {
		return m_Spheros;
	}
	
	/*
	 * ONLY TO BE USED BY SPHERO CONNECTION SCENE 
	 */
	public void setConnectedSpheros(Sphero[] spheros) {
		m_Spheros = spheros;
	}
	
	/*
	 * Call to properly disconnect Spheros.  Call in OnApplicationPause 
	 */
	public void disconnectSpheros() {
		#if UNITY_ANDROID
			// Disconnect robots
			using (AndroidJavaClass jc = new AndroidJavaClass("orbotix.robot.base.RobotProvider"))
			{
		        jc.CallStatic<AndroidJavaObject>("getDefaultProvider").Call("disconnectControlledRobots");	
			}	
		#elif UNITY_IPHONE
		 	
		#endif
	}
}
