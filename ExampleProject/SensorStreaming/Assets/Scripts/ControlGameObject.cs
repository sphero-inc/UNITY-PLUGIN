using UnityEngine;
using System.Collections;

public class ControlGameObject : MonoBehaviour {
	private bool 	observingDataStreaming = false;
	private float 	yaw0 = 0.0f;
	private float 	yaw1 = 0.0f;		
	private Vector3	position = new Vector3(0.0f, 0.0f, 0.0f);
		
	void Update () {
		// Set the updated position
		transform.position = position;
		// Rotate the Sphero for changes in the physical Sphero's yas
		transform.Rotate(new Vector3(0,0,yaw1-yaw0),Space.Self);
		yaw0 = yaw1;
		
		if (!observingDataStreaming) {
			// Start handling streaming events once updates have been started.
			SpheroDeviceMessenger.SharedInstance.AsyncDataReceived += ReceiveAsyncMessage;	
			SpheroDeviceMessenger.SharedInstance.NotificationReceived +=
			 ReceiveNotificationMessage;
			observingDataStreaming = true;
		}
	}
	
	void OnApplicationPause(bool pause) {
		if (pause) {
			// Unregister event handler when application pauses.
			SpheroDeviceMessenger.SharedInstance.AsyncDataReceived -= ReceiveAsyncMessage;
			SpheroDeviceMessenger.SharedInstance.NotificationReceived -=
			 ReceiveNotificationMessage;
		} else {
			// Re-initialize when the application resumes
			observingDataStreaming = false;
			yaw0 = 0.0f;
			yaw1 = 0.0f;
			position = new Vector3(0.0f, 0.0f, 0.0f);
		} 					
	}

	private void ReceiveAsyncMessage(object sender, 
			SpheroDeviceMessenger.MessengerEventArgs eventArgs)
	{
		// Event Handler function 
		SpheroDeviceSensorsAsyncData message = 
			(SpheroDeviceSensorsAsyncData)eventArgs.Message;
		
		SpheroDeviceSensorsData sensorsData = message.Frames[0];
		
		// Get the yaw value which is used to rotate the on screen Sphero
		yaw1 = sensorsData.AttitudeData.Yaw;
		
		// Update the on screen Sphero position using the accelerometer values for x and y
		float xAcceleration = sensorsData.AccelerometerData.Normalized.X;
		float yAcceleration = sensorsData.AccelerometerData.Normalized.Y;
		Vector3 currentPosition = transform.position;
		
		// Create a new position by filtering the accelerometer data using the low pass
		// filtering formula (alpha * filteredValue + (1 - alpha) * newValue)
		position = new Vector3((0.9f * currentPosition.x + 0.1f * xAcceleration), 
			(0.9f * currentPosition.y + 0.1f * yAcceleration), 0.0f);
	}
	
	/*
	 * Callback to receive connection notifications 
	 */
	private void ReceiveNotificationMessage(object sender, SpheroDeviceMessenger.MessengerEventArgs eventArgs)
	{
		SpheroDeviceNotification message = (SpheroDeviceNotification)eventArgs.Message;
		if( message.NotificationType == SpheroDeviceNotification.SpheroNotificationType.DISCONNECTED ) {
			SpheroDeviceMessenger.SharedInstance.AsyncDataReceived -= ReceiveAsyncMessage;
			SpheroDeviceMessenger.SharedInstance.NotificationReceived -=
			 ReceiveNotificationMessage;
		}
	}
}
