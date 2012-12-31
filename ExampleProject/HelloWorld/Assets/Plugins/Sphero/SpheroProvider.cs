using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class SpheroProvider {
	
	// Shared instance of Sphero Provider
	static SpheroProvider sharedProvider = null;
	
	Sphero[] m_Spheros;
	
#if UNITY_ANDROID
	AndroidJavaObject m_RobotProvider;
	AndroidJavaObject m_PairedRobots;
	AndroidJavaObject m_ConnectingRobot;
#endif
	int m_PairedRobotCount = 0;
	
	public SpheroProvider() {}
	
	/* Get the shared RobotProvider instance */
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
		
			// Remove connection listener
			using (AndroidJavaClass jc = new AndroidJavaClass("orbotix.unity.UnityConnectionMessageDispatcher"))
	        {
				AndroidJavaObject jo = jc.CallStatic<AndroidJavaObject>("getDefaultDispatcher");
				jo.Call("removeListener", "GUI");
			}	
		#elif UNITY_IPHONE
		 	
		#endif
	}
	
	
	/* Need to call this to get the robot objects that are paired from Android */
	public void findRobots() {
#if UNITY_ANDROID
		using (AndroidJavaClass jc = new AndroidJavaClass("orbotix.robot.base.RobotProvider"))
        {
			// Grab a handle on the RobotProvider
            m_RobotProvider = jc.CallStatic<AndroidJavaObject>("getDefaultProvider");

			// Only run this stuff if the adapter is enabled
			if( isAdapterEnabled() ) {
				m_RobotProvider.Call("findRobots");  
				m_PairedRobots = m_RobotProvider.Call<AndroidJavaObject>("getRobots");
				m_PairedRobotCount = m_PairedRobots.Call<int>("size");
			}
        }	
#endif		
	}
	
	/* Check if bluetooth is on */
	public bool isAdapterEnabled() {
#if UNITY_ANDROID
		using (AndroidJavaClass jc = new AndroidJavaClass("orbotix.robot.base.RobotProvider"))
        {
			// Grab a handle on the RobotProvider
            m_RobotProvider = jc.CallStatic<AndroidJavaObject>("getDefaultProvider"); 
		}
		return m_RobotProvider.Call<bool>("isAdapterEnabled"); 
#else
		return false;
#endif		
	}
	
	/* Grab the robot names from Java array */
	public string[] getRobotNames() {
		// Store the robots that are paired into an array
		string[] robotNames = new string[m_PairedRobotCount];
#if UNITY_ANDROID		
		for( int i = 0; i < m_PairedRobotCount; i++ ) {
			AndroidJavaObject jo = m_PairedRobots.Call<AndroidJavaObject>("get", i);	
			robotNames[i] = jo.Call<string>("getName");
		}
#endif		
		return robotNames;
	}
	
	/* Grab the connecting Robot */
	public AndroidJavaObject getConnectingRobot() {
#if UNITY_ANDROID
		return m_ConnectingRobot;	
#else
		return null;
#endif
	}
	
	/* Connect to a robot at index */
	public void connect(int index) {
#if UNITY_ANDROID
		// Grab a handle on the RobotProvider
		using (AndroidJavaClass jc = new AndroidJavaClass("orbotix.robot.base.RobotProvider"))
		{
			// Connect the selected robot
    		m_RobotProvider = jc.CallStatic<AndroidJavaObject>("getDefaultProvider");
			m_RobotProvider.Call("control", index);
			m_RobotProvider.Call<AndroidJavaObject>("connectControlledRobots");
			// Save the robot for future calls
			m_ConnectingRobot = m_PairedRobots.Call<AndroidJavaObject>("get",index);
		}	
#endif
	}	
}
