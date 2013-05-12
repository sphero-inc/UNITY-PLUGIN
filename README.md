# Sphero Unity Plugin

## Overview

---

The Sphero Unity Plugin is a group of C# classes that form a bridge between Unity and our native Android™ and iOS™ SDKs.  Unity can call down into each platform's native code for Sphero commands and receive asynchronous data callbacks from native code up into Unity.  The easiest way to get going with the plugin is by starting with one of the samples we have created. If you have an existing project you want to integrate Sphero into, please continue reading. 

	Notice: The Sphero Unity Plugin only works with Android and iOS.
	
## Sphero Unity Samples

---

* [HelloWorld](https://github.com/orbotix/UNITY-PLUGIN/tree/master/ExampleProject/HelloWorld) - Connect to Sphero and blink the RGB LED.  This is the most compact and easy to follow sample for dealing with Sphero. 
 
* [SensorStreaming](https://github.com/orbotix/UNITY-PLUGIN/tree/master/ExampleProject/SensorStreaming) - If you want to use the sensor data from Sphero (to control GameObjects on screen), you should check this sample out. 

* [UISample](https://github.com/orbotix/UNITY-PLUGIN/tree/master/ExampleProject/UISample) - The UISample is a great place to start for drive apps.  It already has elements for a button calibration widget, and a joystick for driving Sphero.		
	
## Adding Sphero to a New or Existing Project

---

Open up Unity and start by choosing File -> New Project.

Next, drag the `Plugins` directory from `Sphero-Unity-Plugin` into the your Unity Project.  If the Unity compiler complains about RegisterDeviceCallback, make sure you are building for either iOS or Android.

### Sphero Connection Scene

We have created scripts for you that handle the connection lifecycle of a Sphero on iOS and Android. The first scene we need to create is the SpheroConnectionScene.  Most of the time, this is the scene that should pop up when you first start the app.  

Start by choosing File -> New Scene, and save it as `SpheroConnectionScene`.  All you need to do is locate the `SpheroConnectionView` prefab that is in the `/Plugins/Resources/Prefabs` directory in your project.  Drag the prefab into the scene.  There are two fields in the inspector that are important to note.  

First, the `Next Level` is a string of the name of the scene that you want Unity to load after the SpheroConnectionScene is done.  It will proceed to the scene after it connects to a Sphero.   

Second, on Android, you have the abilitiy to connect to multiple Spheros, which you enable by checking the `Multiple Spheros` check box. 

Third, the `Splash Screen` Inspector variable takes a Texture2D to display as iOS attempts to connect.  This should be the same splash screen that your application uses if you want the transition to be fluid.

### No Sphero Connected Scene

Next, create another scene and save it as `NoSpheroConnectedScene`.  Drag the `NoSpheroConnectedView` prefab, from the same directory as where you found the SpheroConnectionView, into your scene.  This scene also has a `Next Level` field in the inspector, and it should be the same string value as the one in the SpheroConnectionScene.  On iOS, the SpheroConnectionScene will load the NoSpheroConnectedScene if it can't connect to a Sphero within a second.

![nospheroconnected.png](https://github.com/orbotix/UNITY-PLUGIN/blob/master/Images/NoSpheroConnected.png?raw=true)

This is what you should see on iOS.  The scene is trying to connect to a Sphero when this is running.  On Android, the searching progress is replaced by a connect button that takes the user to the SpheroConnectionScene.

### Using Sphero Objects

The previous scenes will handle Sphero connections.  After a Sphero is connected, your project has access to it through the `SpheroProvider` singleton class.  Access them by this call:

C#

	Sphero[] ConnectedSpheros = SpheroProvider.GetSharedProvider().GetConnectedSpheros();
	Sphero sphero = ConnectedSpheros[0];

JavaScript

	var connectedspheros = new Array();
	var sphero : Sphero;
	connectedspheros = SpheroProvider.GetSharedProvider().GetConnectedSpheros();
	sphero = connectedspheros[0];
		
This array will have a length of 0 if no Spheros are connected. And only ever be a length of 1 on iOS.  It is helpful to check for these conditions and respond in your app accordingly. 

#### Disconnecting Sphero

It is proper Sphero etiquette to shutdown the robot properly.  This ensures that the robot will be left in a stable state when your app is not using it.  In our samples we encourage disconnecting the Spheros in the OnApplicationPause() method, which guarantees the Spheros will be properly disconnected on both platforms.

C#

	void OnApplicationPause(bool pause) {
		if( pause ) {
			SpheroProvider.GetSharedProvider().DisconnectSpheros();
		}
	}

JavaScript

	function OnApplicationPause(paused : boolean) {
		if(paused) {
			SpheroProvider.GetSharedProvider().DisconnectSpheros();
		}
	}

#### Commands

Unlike our native SDKs, commands are sent to the ball through functions in the Sphero Class. All platform dependent code is hidden, so calls to these functions will operate fine on both mobile platforms.  

**Changing Sphero's Color (C# and JavaScript):**

	// Change Sphero color to red
	sphero.SetRGBLED(1.0f, 0.0f, 0.0f);  
**Driving Sphero (C# and JavaScript):**

	// Drive Sphero down -y axis at full speed
	sphero.Roll(180, 1.0f);  
	
There are more commands that you can check out in the Sphero class. 

#### Notifications, Asynchronous Messaging, and Responses

The Sphero SDK sends notifications to Unity when a Sphero disconnects, connects, fails to connect, after every command, and when you are streaming data.  All these messages come into Unity through the `SpheroDeviceMessenger` class.  So, when you are concerned with any of these messages, you simply need to register a callback function with the SpheroDeviceMessenger.  Unfortunately, device messsaging is only supported in C# at the moment.  To register for messaging, add this line of code.

	SpheroDeviceMessenger.SharedInstance.NotificationReceived += ReceiveNotificationMessage;
	
Remember to unregister for the notification when you no longer need it.

	SpheroDeviceMessenger.SharedInstance.NotificationReceived -= Re	ceiveNotificationMessage;

Receive the data and do what you want with it.  Every message has the unique robot id that the message pertains to and a timestamp.  In this instance, there is also a notification type.  The code snippet below sets the state of a Sphero object to disconnected when the disconnect notification message is received.

	private void ReceiveNotificationMessage(object sender, SpheroDeviceMessenger.MessengerEventArgs eventArgs)
	{
		SpheroDeviceNotification message = (SpheroDeviceNotification)eventArgs.Message;
		Sphero notifiedSphero = SpheroProvider.GetSharedProvider().GetSphero(message.RobotID);
		if( message.NotificationType == SpheroDeviceNotification.SpheroNotificationType.DISCONNECTED ) {
			notifiedSphero.ConnectionState = Sphero.Connection_State.Disconnected;
		}
	}

For a walkthrough of the asynchronous message handling, see the SensorStreaming example.

## License

---
The Sphero Unity Plugin is distributed under the Orbotix Source Code License.  Developers are encouraged to help build the plugin and make pull requests to our main Github repository.

## Community and Help

---

* [Developer Forum](http://forum.gosphero.com/) - Share your project, get help and talk to the Sphero developers!

---

*Android™ is a registerd trademark of Google Inc* |
*iOS™ is a licensed trademark of Apple*