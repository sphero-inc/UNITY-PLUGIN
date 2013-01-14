using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HelloWorld : MonoBehaviour {
	
	// The color blue as an int with 100% opacity
	const int BLUE = unchecked((int)0xFF0000FF);
	const int BLACK = unchecked((int)0xFF000000);
	
	// Connected Sphero Robot
	List<Sphero> m_SpheroList;
	
	// Counter to determine if Sphero should have color or not
	int m_BlinkCounter;
	
	// Use this for initialization
	void Start () {
		// Get Connected Sphero
		m_SpheroList = SpheroProvider.GetSharedProvider().GetConnectedSpheros();
		
		// Enable Data Streaming
//		SpheroDeviceMessenger.SharedInstance.AsyncDataReceived += ReceiveAsyncMessage;	
//		foreach( Sphero sphero in m_SpheroList ) {
//			sphero.EnableControllerStreaming(50,1,SpheroDataStreamingMask.IMUPitchAngleFiltered);		
//		}	
	}
	
	// Update is called once per frame
	void Update () {
		m_BlinkCounter++;
		if( m_BlinkCounter % 20 == 0 ) {
			foreach( Sphero sphero in m_SpheroList ) {
				// Set the Sphero color to blue 
				if( sphero.Color == BLACK ) {
					sphero.SetRGBLED(BLUE);
				}
				else {
					sphero.SetRGBLED(BLACK);	
				}
			}
		}
		
#if UNITY_IPHONE
		if( !SpheroBridge.IsRobotConnected() ) {
			// Tell Sphero Provider we have a disconnected robot
			//m_SpheroList[0].ConnectionState = Sphero.Connection_State.Disconnected;
				
			// Load No Sphero Connected Scene
			Application.LoadLevel ("NoSpheroConnectedView"); 
		}
#endif
	}
	
	void OnApplicationPause() {
		// Disconnect robots
		SpheroProvider.GetSharedProvider().DisconnectSpheros();
	}
	
	public void javaMessage(string message) {
		if( message.Equals("disconnect") ) {
			m_SpheroList[0].ConnectionState = Sphero.Connection_State.Disconnected;
			Application.LoadLevel("NoSpheroConnectedScene");
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
	
//	private void ReceiveAsyncMessage(object sender, SpheroDeviceMessenger.MessengerEventArgs eventArgs)
//	{
//		SpheroDeviceSensorsAsyncData message = 
//			(SpheroDeviceSensorsAsyncData)eventArgs.Message;
//		SpheroDeviceSensorsData sensorsData = message.Frames[0];
//		
//		Debug.Log("Sesnsor: "+sensorsData.QuaternionData.Q0);
//	}
}

//using System;
//using UnityEngine;
//using System.Collections;
//
//
//public class UpdateValues: MonoBehaviour {
//	private bool streaming = false;
//	private SpheroAccelerometerData.Acceleration acceleration = 
//		new SpheroAccelerometerData.Acceleration();
//	private float pitch = 0.0f;
//	private float roll = 0.0f;
//	private float yaw = 0.0f;
//	private float q0 = 1.0f;
//	private float q1 = 1.0f;
//	private float q2 = 1.0f;
//	private float q3 = 1.0f;
//	
//	
//	// Use this for initialization
//	void Start () {
//		SpheroDeviceMessenger.SharedInstance.AsyncDataReceived += ReceiveAsyncMessage;	
//	}
//	
//	// Update is called once per frame
//	void Update () {
//		if (SpheroBridge.IsRobotConnected() && !streaming) {
//			SpheroBridge.EnableControllerStreaming(20, 1, 
//				(SpheroDataStreamingMask.IMUAnglesFilteredAll | 
//				SpheroDataStreamingMask.AccelerometerFilteredAll | 
//				SpheroDataStreamingMask.QuaternionAll)); 
//			streaming = true;
//		}	
//	}
//	
//	void OnApplicationPause() {
//		Debug.Log("APPLICATION PAUSE");
//		if (streaming) {
//			SpheroBridge.DisableControllerStreaming();
//			streaming = false;
//		}
//	}
//	
//	void OnApplicationQuit() {
//		Debug.Log("APPLICATION QUIT");
//	}
//	
//	public GUIStyle boxStyle;
//	public GUIStyle labelStyle;
//	
//	void OnGUI() {
////		Vector3 screenScalar = new Vector3(Screen.width/960, Screen.height/640, 1.0f);
////		GUI.matrix = Matrix4x4.Scale(screenScalar);
//		
//		GUI.BeginGroup(new Rect(10, 10, 200, 400), boxStyle);
//		GUI.Box(new Rect(0,0,200, 400), "Stream");
//		GUI.Label(new Rect(4,30,180,36), "accel x: " + acceleration.X, labelStyle);
//		GUI.Label(new Rect(4,66,180,36), "accel y: " + acceleration.Y, labelStyle);
//		GUI.Label(new Rect(4,102,180,36), "accel z: " + acceleration.Z, labelStyle);
//		GUI.Label(new Rect(4,138,180,36), "pitch: " + pitch, labelStyle); 
//		GUI.Label(new Rect(4,174,180,36), "roll: " + roll, labelStyle);
//		GUI.Label(new Rect(4,210,180,36), "yaw: " + yaw, labelStyle);
//		GUI.Label(new Rect(4,246,180,36), "q0: " + q0, labelStyle);
//		GUI.Label(new Rect(4,282,180,36), "q1: " + q1, labelStyle);
//		GUI.Label(new Rect(4,318,180,36), "q2: " + q2, labelStyle);
//		GUI.Label(new Rect(4,354,180,36), "q3: " + q3, labelStyle);
//		GUI.EndGroup();
//	}
//	
//	private void ReceiveAsyncMessage(object sender, 
//			SpheroDeviceMessenger.MessengerEventArgs eventArgs)
//	{
//		SpheroDeviceSensorsAsyncData message = 
//			(SpheroDeviceSensorsAsyncData)eventArgs.Message;
//		SpheroDeviceSensorsData sensorsData = message.Frames[0];
//		
//		acceleration = sensorsData.AccelerometerData.Normalized;
//		
//		pitch = sensorsData.AttitudeData.Pitch;
//		roll = sensorsData.AttitudeData.Roll;
//		yaw = sensorsData.AttitudeData.Yaw;
//		
//		q0 = sensorsData.QuaternionData.Q0;
//		q1 = sensorsData.QuaternionData.Q1;
//		q2 = sensorsData.QuaternionData.Q2;
//		q3 = sensorsData.QuaternionData.Q3; 
//		
//	}
//
//}
