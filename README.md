# Sphero Unity Plugin

---
This plugin is meant to be open source.  The underlying architecture is in place; however, it isn't as complete as our iOS and Android SDKs.  We want the community to be involved with the Unity Plugin's progression and development.  We will be monitoring pull request on the Github Repo, so feel free to make changes.  

## Community and Help

---

* [Developer Forum](http://forum.gosphero.com/) - Share your project, get help and talk to the Sphero developers!

## Samples

---

 * [HelloWorld](https://github.com/orbotix/Sphero-Unity-Plugin/tree/master/ExampleProject/HelloWorld) - Connect to Sphero and blink the LED.  This is the most compact and easy to follow sample for dealing with Sphero. 
 
* [SensorStreaming](https://github.com/orbotix/Sphero-Unity-Plugin/tree/master/ExampleProject/SensorStreaming) - If you want to use the sensor data from Sphero (to control GameObjects on screen), you should check this sample out. 

* [UISample](https://github.com/orbotix/Sphero-Unity-Plugin/tree/master/ExampleProject/RobotUISample) - The UISample is a great place to start for drive apps.  It already has elements for button calibration widget, and also has a joystick for driving Sphero.

## Overview

---

The Sphero Unity Plugin is essentially a group of C# classes that form a bridge between Unity and our Native Android and iOS SDKs.  Unity can call down into each platform's native code for Sphero commands and even receive asynchronous data callbacks from native code up into Unity.  Standard SDK architecture hides implementation from the developer, and we tried to do just that with the Unity Plugin.  The developer should not have to change any code if they are deploying to Android or iOS.  

	Note: The Sphero Unity Plugin only works with Android and iOS

## Adding to the Plugin

With our Android and iOS SDK, we do not explain the inner mechanics of the code.  However, since we want this to be an open source project, we feel it is necessary to explain a bit what we have done.  For the most part being, there is a large shortage of tutorials on Unity Plugins.  

Unity's project structure requires you to place any plugin code in the plugin directory.  The code inside here is compiled to be accessed from C#, javascript, or boo.  Therefore, even though we made our samples in C#, the Sphero Unity Plugin should work with any other Unity coding language.  Let's start with iOS, because this probably the easier plugin to explain.

### iOS Plugin

In the Plugin directory in the Unity Project Structure, there is another directory entitled "iOS".  Unity looks for this and knows to add it when you are deploying on an iPhone, iPad, or iPod Touch.  We place in here the Sphero iOS Native SDK and the ".mm" file (Objective-C that can also have C++ code) that bridges the gap.

For this code, we mostly followed the guidelines on [Unity Plugin Manuel](http://docs.unity3d.com/Documentation/Manual/Plugins.html).  The process is to define extern "C" {} functions and calling them in Unity by defining them as such:


	[DllImport ("__Internal")]
	private static extern float FooPluginFunction ();
	
This is all there is to it to calling from Unity down into Native iOS code.  So, to add additionaly Sphero commands, add functions into the RKUNBridge.mm file.

#### Calling C#/Javascript back from Native iOS Code

The standard way to call up into Unity is by using UnitySendMessage.  However, after some thought and reflection, we decided against it.  For one, we believe it uses reflection to find the game object and method name to send the message to.  Which, in the case of data streaming, is too slow.  Also, we wanted the developer to be able to process the data at the rate it came in, and as far as we could tell UnitySendMessage only executes alongside the Update() method.
  
Therefore, we decided to use a function pointer to pass data from Native Code into Unity.  We used the MonoPInvokeCallback attribute to pass a C# function pointer from managed to unmanaged code.  You can read more about this [here](http://docs.go-mono.com/?link=T%3aMonoTouch.MonoPInvokeCallbackAttribute).  The C# code then gives a function pointer to the RKUNBRidge class.  It uses the pointer to make callbacks into C#.  

Using this technique, we are able to send notifications, responses, and asynchronous data from native code back up into Unity as encoded strings.  Theses encoded strings then get decoded in the SpheroDeviceMessenger class, where they are distributed to the necessary Unity project classes using EventHandlers.

### Android Plugin

The Android plugin is more advanced, because it requires one to be familiar with the Android NDK and the JNI.  Luckily, Unity has made it easy to call into the JNI with the AndroidJavaObject and AndroidJavaClass objects.  You can learn more about how to use them from Unity's website [here](http://docs.unity3d.com/Documentation/Manual/PluginsForAndroid.html)

#### Calling C#/Javascript back from Native Android Code

Once again, the standard way to do this is with UnitySendMessage.  Thus, for the same reasons as above, we have decided to use MonoPInvokeCallback between C++ and C# code.  Hence, we are going to have to compile a shared library for native Android to communcate 

 
 
### Sphero Connection View


