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
	
	/*
	 * More detailed constructor used for Android 
	 */ 
	public SpheroAndroid(AndroidJavaObject sphero, string bt_name, string bt_address) : base() {		
		m_AndroidJavaSphero = sphero;
		m_DeviceInfo = new BluetoothDeviceInfo(bt_name, bt_address);
	}
	
	/*
	 * Change Sphero's color to desired output
	 * @param red the amount of red from (0.0 - 1.0) intensity
	 * @param green the amount of green from (0.0 - 1.0) intensity
	 * @param blue the amount of blue from (0.0 - 1.0) intensity
	 */
	override public void SetRGBLED(float red, float green, float blue) {
		m_RGBLEDOutput.CallStatic("sendCommand",m_AndroidJavaSphero,(int)(red*255),(int)(green*255),(int)(blue*255));
		m_RGBLEDColor = new Color(red, green, blue, 1.0f);
	}
	
	/**
	 * Enable controller data streaming with infinite packets
     * @param divisor Divisor of the maximum sensor sampling rate (400 Hz)
     * @param packetFrames Number of samples created on the device before it sends a packet to the phone with samples
     * @param sensorMask Bitwise selector of data sources to stream
	 */
	override public void EnableControllerStreaming(ushort divisor, ushort packetFrames, SpheroDataStreamingMask sensorMask) {			
		m_UnityBridge.Call("enableControllerStreaming",m_AndroidJavaSphero,(int)divisor,(int)packetFrames,(long)sensorMask);
	}
	
	/**
	 * Disable controller data streaming
	 */
	override public void DisableControllerStreaming() {
		m_UnityBridge.Call("disableControllerStreaming",m_AndroidJavaSphero);
	}
	
	/**
	 * Start Streaming Data to Unity
     * @param divisor Divisor of the maximum sensor sampling rate (400 Hz)
     * @param packetFrames Number of samples created on the device before it sends a packet to the phone with samples
     * @param sensorMask Bitwise selector of data sources to stream
     * @param packetCount Packet count (set to 0 for unlimited streaming) 
	 */
	override public void SetDataStreaming(ushort divisor, ushort packetFrames, SpheroDataStreamingMask sensorMask, ushort packetCount) {
		m_UnityBridge.Call("setDataStreaming",m_AndroidJavaSphero, divisor, packetFrames, sensorMask, packetCount);
	}
}

#endif