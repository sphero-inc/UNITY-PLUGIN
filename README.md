# Sphero Unity Plugin

---
This plugin is meant to be open source.  The underlying architecture is in place; however, it isn't as complete as our iOS and Android SDKs.  We want the community to be involved with the Unity Plugin's progression and development.  We will be monitoring pull request on the Github Repo, so feel free to make changes.  

## Community and Help

---

* [Developer Forum](http://forum.gosphero.com/) - Share your project, get help and talk to the Sphero developers!


## Overview

---

The Sphero Unity Plugin is essentially a group of C# classes that form a bridge between Unity and our Native Android and iOS SDKs.  Unity can call down into each platform's native code for Sphero commands and receive asynchronous data callbacks from native code up into Unity.  The easiest way to get going with the plugin is by starting with on of the samples we have created. If you have an existing project you want to integrate Sphero into, please continue reading. 

	Notice: The Sphero Unity Plugin only works with Android and iOS
	
## Sphero Unity Samples

---

 * [HelloWorld](https://github.com/orbotix/Sphero-Unity-Plugin/tree/master/ExampleProject/HelloWorld) - Connect to Sphero and blink the RGB LED.  This is the most compact and easy to follow sample for dealing with Sphero. 
 
* [SensorStreaming](https://github.com/orbotix/Sphero-Unity-Plugin/tree/master/ExampleProject/SensorStreaming) - If you want to use the sensor data from Sphero (to control GameObjects on screen), you should check this sample out. 

* [UISample](https://github.com/orbotix/Sphero-Unity-Plugin/tree/master/ExampleProject/RobotUISample) - The UISample is a great place to start for drive apps.  It already has elements for a button calibration widget, and a joystick for driving Sphero.		
	
## Adding Sphero to a New or Existing Project

---

Open up Unity and start by choosing File -> New Project.

Next, drag the `Plugins` directory from `Sphero-Unity-Plugin` into the your Unity Project.  If the Unity compiler complains about RegisterDeviceCallback, make sure you are building for either iOS or Android.

### Sphero Connection Scene

We have created scripts for you that handle the connection lifecycle of a Sphero on iOS and Android. The first scene we need to create is the SpheroConnectionScene.  Most of the time, this is the scene that should pop up when you first start the app.  

Start by choosing File -> New Scene, and save it as `SpheroConnectionScene`.  All you need to do is locate the `SpheroConnectionView` prefab that is in the `/Plugins/Resources/Prefabs` directory in your project.  Drag the prefab into the scene.  There are two fields in the inspector that are important to note.  

First, the `Next Level` is a string of the name of the scene that you want Unity to load after the SpheroConnectionScene is done.  It will proceed to the scene after it connects to a Sphero.   

Second, on Android, you have the abilitiy to connect to multiple Spheros, which you enable by checking the `Multiple Spheros` check box. 

### No Sphero Connected Scene

Next, create another scene and save it as `NoSpheroConnectedScene`.  Drag the `NoSpheroConnectedView` prefab, from the same directory as where you found the SpheroConnectionView, into your scene.  This scene also has a `Next Level` field in the inspector, and it should be the same string value as the one in the SpheroConnectionScene.  On iOS, the SpheroConnectionScene will load the NoSpheroConnectedScene if it can't connect to a Sphero within a second.

![nospheroconnected.png](https://github.com/orbotix/Sphero-Unity-Plugin/raw/master/assets/image01.png)

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

It is proper Sphero edicate to shutdown the robot properly.  This ensures that the robot will be left in a stable state when your app is not using it.  In our samples we encourage disconnecting the Spheros in the OnApplicationPause() method, which gurentees the Spheros will be properly disconnected on both platforms.

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

Receive the data and do what you want with it.  Every message has the unique robot id that the message pertains too and a timestamp.  In this instance, there is also a notification type.  The code snippet below sets the state of a Sphero object to disconnected when the disconnect notification message is received.

	private void ReceiveNotificationMessage(object sender, SpheroDeviceMessenger.MessengerEventArgs eventArgs)
	{
		SpheroDeviceNotification message = (SpheroDeviceNotification)eventArgs.Message;
		Sphero notifiedSphero = SpheroProvider.GetSharedProvider().GetSphero(message.RobotID);
		if( message.NotificationType == SpheroDeviceNotification.SpheroNotificationType.DISCONNECTED ) {
			notifiedSphero.ConnectionState = Sphero.Connection_State.Disconnected;
		}
	}

For a walkthrough of the asynchronous message handling, see the SensorStreaming example.

## Automatic Building on iOS

---
When building for iOS, XCode requires a few extra frameworks to work with our `RobotKit.framework` (iOS Native Sphero SDK).  So, we wrote a post process build script that you should include in your Unity project if you want to be able to hit "Build And Run…" and not have to add the RobotKit.framework, ExternalAccessory.framework, and the supported external accessory protocol of "com.orbotix.robotprotocol" in the info.plist.

Simply drag the Editor folder found in the Assets folder of any of the examples in the Sphero-Unity-Plugin into your project's Assets directory.