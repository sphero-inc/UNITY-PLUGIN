using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

#if UNITY_ANDROID

public class SpheroAndroid : Sphero {
	
	private AndroidJavaObject m_AndroidJavaSphero;
	public AndroidJavaObject AndroidJavaSphero
    {
		get{ return this.m_AndroidJavaSphero; }
    }
	
	private AndroidJavaObject m_UnityBridge = (new AndroidJavaClass("orbotix.unity.UnityBridge")).CallStatic<AndroidJavaObject>("sharedBridge");
	
	// Cached Java Classes for efficient calls
	private AndroidJavaClass m_RGBLEDOutput = new AndroidJavaClass("orbotix.robot.base.RGBLEDOutputCommand");
	private AndroidJavaClass m_RollCommand = new AndroidJavaClass("orbotix.robot.base.RollCommand");
	private AndroidJavaClass m_SetHeadingCommand = new AndroidJavaClass("orbotix.robot.base.SetHeadingCommand");
	private AndroidJavaClass m_BackLEDOutputCommand = new AndroidJavaClass("orbotix.robot.base.BackLEDOutputCommand");
	
	/* More detailed constructor used for Android */ 
	public SpheroAndroid(AndroidJavaObject sphero, string bt_name, string bt_address) : base() {		
		m_AndroidJavaSphero = sphero;
		m_DeviceInfo = new BluetoothDeviceInfo(bt_name, bt_address);
	}
	
	override public void SetRGBLED(float red, float green, float blue) {
		m_RGBLEDOutput.CallStatic("sendCommand",m_AndroidJavaSphero,(int)(red*255),(int)(green*255),(int)(blue*255));
		m_RGBLEDColor = new Color(red, green, blue, 1.0f);
	}

	override public void EnableControllerStreaming(ushort divisor, ushort packetFrames, SpheroDataStreamingMask sensorMask) {			
		m_UnityBridge.Call("enableControllerStreaming",m_AndroidJavaSphero,(int)divisor,(int)packetFrames,(long)sensorMask);
	}

	override public void DisableControllerStreaming() {
		m_UnityBridge.Call("disableControllerStreaming",m_AndroidJavaSphero);
	}
	
	override public void SetDataStreaming(ushort divisor, ushort packetFrames, SpheroDataStreamingMask sensorMask, ushort packetCount) {
		m_UnityBridge.Call("setDataStreaming",m_AndroidJavaSphero, divisor, packetFrames, sensorMask, packetCount);
	}
	
	override public void Roll(int heading, float speed) {
		m_RollCommand.CallStatic("sendCommand",m_AndroidJavaSphero,(float)heading,speed);
	}
	
	override public void SetHeading(int heading) {
		m_SetHeadingCommand.CallStatic("sendCommand",m_AndroidJavaSphero,(float)heading);
	}
	
	override public void SetBackLED(float intensity) {
		m_BackLEDOutputCommand.CallStatic("sendCommand",m_AndroidJavaSphero,intensity);
	}
}

#endif