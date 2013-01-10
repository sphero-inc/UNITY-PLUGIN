using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System.Collections.Generic;

public class SpheroProvider {
	
	// Shared instance of Sphero Provider
	static SpheroProvider sharedProvider = null;
	
#if UNITY_ANDROID
	private AndroidJavaObject m_RobotProvider;
#endif
	
	// Robots
	Sphero[] m_PairedSpheros;
	
	// The Sphero that is currently attempting to connect
	Sphero m_ConnectingSphero;
	
	/*
	 * Get the Robot Provider for Android 
	 */
	public SpheroProvider() {
		// The SDK uses alot of handlers that need a valid Looper in the thread, so set that up here
        using (AndroidJavaClass jc = new AndroidJavaClass("android.os.Looper"))
        {
        	jc.CallStatic("prepare");
        }
		
		using (AndroidJavaClass jc = new AndroidJavaClass("orbotix.robot.base.RobotProvider"))
	    {
			m_RobotProvider = jc.CallStatic<AndroidJavaObject>("getDefaultProvider");
		}
	}
	
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
		List<Sphero> connectedSpheros = new List<Sphero>();
		#if UNITY_ANDROID
			foreach( Sphero sphero in m_PairedSpheros ) {
				if( sphero.ConnectionState == Sphero.Connection_State.Connected ) {
					connectedSpheros.Add(sphero);	
				}
			}
		#elif UNITY_IOS
			connectedSpheros.Add(m_PairedSpheros[0]);
		#endif		
		return connectedSpheros;
	}
	
	/*
	 * ONLY TO BE USED BY SPHERO CONNECTION SCENE 
	 */
	public void AddConnectedSphero(Sphero sphero) {
		sphero.ConnectionState = Sphero.Connection_State.Connected;
		#if UNITY_ANDROID
			// Needs to be changed in the future
		#elif UNITY_IPHONE						
			m_PairedSpheros = new Sphero[1];
			m_PairedSpheros[0] = sphero;
		#endif
	}
	
	/*
	 * Call to properly disconnect Spheros.  Call in OnApplicationPause 
	 */
	public void DisconnectSpheros() {
		#if UNITY_ANDROID
			// Disconnect robots
		    m_RobotProvider.Call("disconnectControlledRobots");	
		#elif UNITY_IPHONE
		 	
		#endif
	}
	
	
	/* Need to call this to get the robot objects that are paired from Android */
	public bool FindRobots() {
		#if UNITY_ANDROID
			// Only run this stuff if the adapter is enabled
			if( IsAdapterEnabled() ) {
				m_RobotProvider.Call("findRobots");  
				AndroidJavaObject pairedRobots = m_RobotProvider.Call<AndroidJavaObject>("getRobots");
				int pairedRobotCount = pairedRobots.Call<int>("size");
				// Initialize Sphero array
				m_PairedSpheros = new Sphero[pairedRobotCount];
				// Create Sphero objects for the Paired Spheros
				for( int i = 0; i < pairedRobotCount; i++ ) {
					// Set up the Sphero objects
					AndroidJavaObject robot = pairedRobots.Call<AndroidJavaObject>("get",i);
					string bt_name = robot.Call<string>("getName");
					string bt_address = robot.Call<string>("getUniqueId");
					m_PairedSpheros[i] = new Sphero(robot, bt_name, bt_address);
				}
				return true;
			}
		#endif		
		return false;
	}
	
	/* Check if bluetooth is on */
	public bool IsAdapterEnabled() {
		#if UNITY_ANDROID
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
		string[] robotNames = new string[m_PairedSpheros.Length];
		#if UNITY_ANDROID		
			for( int i = 0; i < m_PairedSpheros.Length; i++ ) {
				robotNames[i] = m_PairedSpheros[i].GetName();
			}
		#endif		
		return robotNames;
	}
	
	/* Grab the connecting Robot */
	public Sphero GetConnectingSphero() {
		#if UNITY_ANDROID
			foreach( Sphero sphero in m_PairedSpheros ) {
				if( sphero.ConnectionState == Sphero.Connection_State.Connecting ) {
					return sphero;
				}	
			}
		#else
			return null;
		#endif
		return null;
	}
	
	/* Connect to a robot at index */
	public void Connect(int index) {
		// Don't try to connect to multiple Spheros at once
		if( GetConnectingSphero() != null ) return;
		#if UNITY_ANDROID
			m_RobotProvider.Call("control", m_PairedSpheros[index].AndroidJavaSphero);
			m_RobotProvider.Call<AndroidJavaObject>("connectControlledRobots");
			m_PairedSpheros[index].ConnectionState = Sphero.Connection_State.Connecting;
		#endif
	}	
	
	/*
	 * Set the connection state of a paired robot 
	 */
	public void SetRobotConnectionState(int index, Sphero.Connection_State connectionState) {
		m_PairedSpheros[index].ConnectionState = connectionState;
	}
}
