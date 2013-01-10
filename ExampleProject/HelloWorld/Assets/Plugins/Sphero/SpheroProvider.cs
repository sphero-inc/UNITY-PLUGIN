using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System.Collections.Generic;

public class SpheroProvider {
	
	// Shared instance of Sphero Provider
	static SpheroProvider sharedProvider = null;
	
#if UNITY_ANDROID
	AndroidJavaObject m_RobotProvider;
#endif
	
	// Robots
	private List<Sphero> m_ConnectedSpheros = new List<Sphero>();
	Sphero[] m_PairedSpheros;
	
	Sphero m_ConnectingSphero;
	
	int m_PairedRobotCount = 0;
	
	public SpheroProvider() {}
	
	/* Get the shared RobotProvider instance */
	public static SpheroProvider GetSharedProvider() {
		if( sharedProvider == null ) {
			sharedProvider = new SpheroProvider();
		}
		return sharedProvider;
	}
	
	/*
	 * Get the robots that were connected by the Sphero Connection Scene 
	 */  
	public List<Sphero> GetConnectedSpheros() {
		return m_ConnectedSpheros;
	}
	
	/*
	 * ONLY TO BE USED BY SPHERO CONNECTION SCENE 
	 */
	public void AddConnectedSphero(Sphero sphero) {
		m_ConnectedSpheros.Add(sphero);
	}
	
	/*
	 * Call to properly disconnect Spheros.  Call in OnApplicationPause 
	 */
	public void DisconnectSpheros() {
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
	public void FindRobots() {
#if UNITY_ANDROID
		using (AndroidJavaClass jc = new AndroidJavaClass("orbotix.robot.base.RobotProvider"))
        {
			// Grab a handle on the RobotProvider
            m_RobotProvider = jc.CallStatic<AndroidJavaObject>("getDefaultProvider");

			// Only run this stuff if the adapter is enabled
			if( IsAdapterEnabled() ) {
				m_RobotProvider.Call("findRobots");  
				AndroidJavaObject pairedRobots = m_RobotProvider.Call<AndroidJavaObject>("getRobots");
				m_PairedRobotCount = pairedRobots.Call<int>("size");
				// Initialize Sphero array
				m_PairedSpheros = new Sphero[m_PairedRobotCount];
				// Create Sphero objects for the Paired Spheros
				for( int i = 0; i < m_PairedRobotCount; i++ ) {
					// Set up the Sphero objects
					AndroidJavaObject robot = pairedRobots.Call<AndroidJavaObject>("get",i);
					string bt_name = robot.Call<string>("getName");
					string bt_address = robot.Call<string>("getBluetoothName");
					m_PairedSpheros[i] = new Sphero(robot, bt_name, bt_address);
				}
			}
        }	
#endif		
	}
	
	/* Check if bluetooth is on */
	public bool IsAdapterEnabled() {
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
	
	/*
	 * Get the Paired Spheros
	 */
	public Sphero[] GetPairedSpheros() {
		return m_PairedSpheros;	
	}
	
	/* Grab the robot names from Java array */
	public string[] GetRobotNames() {
		// Store the robots that are paired into an array
		string[] robotNames = new string[m_PairedRobotCount];
#if UNITY_ANDROID		
		for( int i = 0; i < m_PairedRobotCount; i++ ) {
			robotNames[i] = m_PairedSpheros[i].GetName();
		}
#endif		
		return robotNames;
	}
	
	/* Grab the connecting Robot */
	public Sphero GetConnectingSphero() {
#if UNITY_ANDROID
		return m_ConnectingSphero;	
#else
		return null;
#endif
	}
	
	/* Connect to a robot at index */
	public void Connect(int index) {
#if UNITY_ANDROID
		// Grab a handle on the RobotProvider
		using (AndroidJavaClass jc = new AndroidJavaClass("orbotix.robot.base.RobotProvider"))
		{
			// Connect the selected robot
    		m_RobotProvider = jc.CallStatic<AndroidJavaObject>("getDefaultProvider");
			m_RobotProvider.Call("control", index);
			m_RobotProvider.Call<AndroidJavaObject>("connectControlledRobots");
			// Save the robot for future calls
			m_ConnectingSphero = m_PairedSpheros[index];
		}	
#endif
	}	
	
	/*
	 * Set the connection state of a paired robot 
	 */
	public void SetRobotConnectionState(int index, Sphero.Connection_State connectionState) {
		m_PairedSpheros[index].ConnectionState = connectionState;
	}
}
