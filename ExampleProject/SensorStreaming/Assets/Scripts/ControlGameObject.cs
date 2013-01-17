using UnityEngine;
using System.Collections;

public class ControlGameObject : MonoBehaviour {
	private float yaw0 = 0.0f;
	private float yaw1 = 0.0f;
	
	private float xOffset = 0.0f;
	private float yOffset = 0.0f;
	
	void Start () {
		SpheroDeviceMessenger.SharedInstance.AsyncDataReceived += ReceiveAsyncMessage;		
	}
	
	void Update () {
		transform.Rotate(new Vector3(0,0,yaw1-yaw0),Space.Self);
		transform.Translate(xOffset, 0, 0, Space.World);
//		transform.Translate(0, yOffset, 0, Space.World);s
		yaw0 = yaw1;
	}
	
	private void ReceiveAsyncMessage(object sender, 
			SpheroDeviceMessenger.MessengerEventArgs eventArgs)
	{
		SpheroDeviceSensorsAsyncData message = 
			(SpheroDeviceSensorsAsyncData)eventArgs.Message;
		SpheroDeviceSensorsData sensorsData = message.Frames[0];
		
		yaw1 = sensorsData.AttitudeData.Yaw;

		
		//
		float xAcceleration = sensorsData.AccelerometerData.Normalized.X;
		float yAcceleration = sensorsData.AccelerometerData.Normalized.Y;
		

		xOffset = 0.3f * xAcceleration;
		if (transform.position.x + xOffset > 1.0) {
			xOffset = 1.0f - transform.position.x;
		} else if ( transform.position.x + xOffset < -1.0 ) {
			xOffset = -1.0f - transform.position.y;
		}
		yOffset = 0.3f * yAcceleration;
		
		
	}
}
