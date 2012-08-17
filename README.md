### **Sphero Unity plugin**
NOTE: This plugin is a collection of experiments we have conducted with Unity and Sphero.  The plugin is likely to completely change before we release a final version.  This version was only intended for experimenting at the hackathon.  The iOS version is the most tested and stable at this point.

This repo contains a sample Unity project that will connect to Sphero on both platforms, present a joystick to drive the ball and cause the ball to blink red, green and blue.  A few c# scripts are provided for the joystick and blinking the ball.  You will use the RKUNBridge c# class for communicating with Sphero.

## Android:
* Only one (1) Sphero can be paired with the Android device.  Un-pair any other Spheros before attempting to use the app.
* Data streaming isn't working yet on Android.  You will have to write a plugin to bridge this gap for now.
* A black screen will appear while the phone is connecting to the ball. Once it connects this will disappear showing you a joystick.

## iOS:
* Using Sphero in an iOS application requires some special setup in the xcode project that Unity doesn't support.  The example project contains a Python PostProcessBuildPlayer script that will configure these settings for you automatically.  You will need to have Python 2.7.x installed using the installer package (installing from the command line won't work for some reason).  You can grab the latest here http://www.python.org/ftp/python/2.7.3/python-2.7.3-macosx10.6.dmg
*  When building your xcode project from Unity place the xcode project in a folder at the same level as your project.  (e.g. UnityPlugin/ExampleProject/xcodeproj/project.xcodeproj)  This is required for the Python script that sets up the xcode project to work.
* Data streaming works, enable data streaming in RKUNBridge.\_enableDataStreaing() and then poll the latest sensor data using RKUNBridge.\_RKUNSensorData();
*Feel free to modify RKUNBridge.h/mm/cs and add more functionality.