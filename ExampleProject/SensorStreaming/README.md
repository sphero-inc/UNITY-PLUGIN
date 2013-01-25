# Streaming Example

---
The streaming example demonstrates how to enable data streaming for controller applications. The application displays the values returned for data, and a Sphero™ model is moved around the screen using the accelerometer's x and y values and is rotated using the attitude's yaw value. This sample code can be used as a basis for demonstrating how to control a game object in a game. 


## Data Streaming

---
The code that initiates data streaming is in UpdateValues.cs which is a MonoBehaviour that is attached to the Main Camera. This script displays the streamed data values too.

In the Update method data streaming is initiated after a Sphero is connected. First an event handler callback is added to the SpheroDeviceMessenger singleton.
			
	SpheroDeviceMessenger.SharedInstance.AsyncDataReceived += ReceiveAsyncMessage;	
The event handler needs to conform to the following delegate signature.

	public delegate void MessengerEventHandler(object sender, MessengerEventArgs eventArgs);

After registering the event handler, the connected Sphero object is retrieved from the SpheroProvider singleton. Then controller mode data streaming is enabled.

	m_Sphero.EnableControllerStreaming(20, 1,
			SpheroDataStreamingMask.AccelerometerFilteredAll |
			SpheroDataStreamingMask.QuaternionAll |
			SpheroDataStreamingMask.IMUAnglesFilteredAll);
			
Enabling controller streaming turns off stabilization. This turns off the control system, so it will not try to maintain the drive mechanism in a fix postion which will prevent the data from changing. Then, it turns on the blue back LED which provides you with a reference for the sensors' negative x axes. Finally, the data streaming command is sent with the sample rate at 20 samples per second (400/20), 1 sample per sent packet, and acceleromter, IMU (attitude), and Quaternion data turned on. 


Streaming is disabled if the application pauses (is backgrounded) and streaming event handler is removed.

	// removes data streaming event handler
	SpheroDeviceMessenger.SharedInstance.AsyncDataReceived -= 
			ReceiveAsyncMessage;	
	// Turns off controller mode data streaming. Stabilization is 
	// restored and the back LED is turn off.
	m_Sphero.DisableControllerStreaming();

## Moving a Game Object

---

The script ControlGameObject.cs is attached to the Sphero game object. This code registers an async data event handler with the SpheroDeviceMessenger singleton. As it receives data streaming messages it assigns yaw, x acceleration, and y acceleration to private members.

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


These are used in the Update method to change the location and rotation of the Sphero model. 
		

## Community and Help

---

* [Developer Forum](http://forum.gosphero.com/) - Share your project, get help and talk to the Sphero developers!