using System;
using UnityEngine;
using System.Collections;


public class UpdateValues: MonoBehaviour {
	private bool streaming = false;
	private SpheroAccelerometerData.Acceleration acceleration = 
		new SpheroAccelerometerData.Acceleration();
	private float pitch = 0.0f;
	private float roll = 0.0f;
	private float yaw = 0.0f;
	private float q0 = 1.0f;
	private float q1 = 1.0f;
	private float q2 = 1.0f;
	private float q3 = 1.0f;
	
	
	// Use this for initialization
	void Start () {
		SpheroDeviceMessenger.SharedInstance.AsyncDataReceived += ReceiveAsyncMessage;	
	}
	
	// Update is called once per frame
	void Update () {
		if (SpheroBridge.IsRobotConnected() && !streaming) {
			SpheroBridge.EnableControllerStreaming(20, 1, 0xF00000000007E000); 
			streaming = true;
		}	
	}
	
	void OnApplicationPause() {
		Debug.Log("APPLICATION PAUSE");
		if (streaming) {
			SpheroBridge.DisableControllerStreaming();
			streaming = false;
		}
	}
	
	void OnApplicationQuit() {
		Debug.Log("APPLICATION QUIT");
	}
	
	public GUIStyle boxStyle;
	public GUIStyle labelStyle;
	
	void OnGUI() {
//		Vector3 screenScalar = new Vector3(Screen.width/960, Screen.height/640, 1.0f);
//		GUI.matrix = Matrix4x4.Scale(screenScalar);
		
		GUI.BeginGroup(new Rect(10, 10, 200, 400), boxStyle);
		GUI.Box(new Rect(0,0,200, 400), "Stream");
		GUI.Label(new Rect(4,30,180,36), "accel x: " + acceleration.X, labelStyle);
		GUI.Label(new Rect(4,66,180,36), "accel y: " + acceleration.Y, labelStyle);
		GUI.Label(new Rect(4,102,180,36), "accel z: " + acceleration.Z, labelStyle);
		GUI.Label(new Rect(4,138,180,36), "pitch: " + pitch, labelStyle); 
		GUI.Label(new Rect(4,174,180,36), "roll: " + roll, labelStyle);
		GUI.Label(new Rect(4,210,180,36), "yaw: " + yaw, labelStyle);
		GUI.Label(new Rect(4,246,180,36), "q0: " + q0, labelStyle);
		GUI.Label(new Rect(4,282,180,36), "q1: " + q1, labelStyle);
		GUI.Label(new Rect(4,318,180,36), "q2: " + q2, labelStyle);
		GUI.Label(new Rect(4,354,180,36), "q3: " + q3, labelStyle);
		GUI.EndGroup();
	}
	
		private void ReceiveAsyncMessage(object sender, 
			SpheroDeviceMessenger.MessengerEventArgs eventArgs)
	{
		SpheroDeviceSensorsAsyncData message = 
			(SpheroDeviceSensorsAsyncData)eventArgs.Message;
		SpheroDeviceSensorsData sensorsData = message.Frames[0];
		
		acceleration = sensorsData.AccelerometerData.Normalized;
		
		pitch = sensorsData.AttitudeData.Pitch;
		roll = sensorsData.AttitudeData.Roll;
		yaw = sensorsData.AttitudeData.Yaw;
		
		q0 = sensorsData.QuaternionData.Q0;
		q1 = sensorsData.QuaternionData.Q1;
		q2 = sensorsData.QuaternionData.Q2;
		q3 = sensorsData.QuaternionData.Q3; 
		
	}

}
