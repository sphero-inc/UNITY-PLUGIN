using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

#if UNITY_IPHONE

public class SpheroIOS : Sphero {
	
	/*
	 * Default constructor used for iOS 
	 */ 
	public SpheroIOS() : base() {
		m_DeviceInfo = new BluetoothDeviceInfo("", "");
	}
	
	/*
	 * Change Sphero's color to desired output
	 * @param red the amount of red from (0.0 - 1.0) intensity
	 * @param green the amount of green from (0.0 - 1.0) intensity
	 * @param blue the amount of blue from (0.0 - 1.0) intensity
	 */
	override public void SetRGBLED(float red, float green, float blue) {
		setRGB(red,green,blue);
		m_RGBLEDColor = new Color(red, green, blue, 1.0f);
	}
	
	/**
	 * Enable controller data streaming with infinite packets
     * @param divisor Divisor of the maximum sensor sampling rate (400 Hz)
     * @param packetFrames Number of samples created on the device before it sends a packet to the phone with samples
     * @param sensorMask Bitwise selector of data sources to stream
	 */
	override public void EnableControllerStreaming(ushort divisor, ushort packetFrames, SpheroDataStreamingMask sensorMask) {		
		enableControllerStreaming(divisor, packetFrames, sensorMask);
	}
	
	/**
	 * Disable controller data streaming
	 */
	override public void DisableControllerStreaming() {
		disableControllerStreaming();
	}
	
	/**
	 * Start Streaming Data to Unity
     * @param divisor Divisor of the maximum sensor sampling rate (400 Hz)
     * @param packetFrames Number of samples created on the device before it sends a packet to the phone with samples
     * @param sensorMask Bitwise selector of data sources to stream
     * @param packetCount Packet count (set to 0 for unlimited streaming) 
	 */
	override public void SetDataStreaming(ushort divisor, ushort packetFrames, SpheroDataStreamingMask sensorMask, ushort packetCount) {
		setDataStreaming(divisor, packetFrames, sensorMask, (byte)packetCount);
	}
	
	/* Native Bridge Functions from RKUNBridge.mm */
	[DllImport ("__Internal")]
	private static extern void setRGB(float red, float green, float blue);
	[DllImport ("__Internal")]
	private static extern void roll(int heading, float speed);
	[DllImport ("__Internal")]
	private static extern void setHeading(int heading);
	[DllImport ("__Internal")]
	private static extern void setBackLED(float intensity);
	[DllImport ("__Internal")]
	private static extern void setDataStreaming(ushort sampleRateDivisor, 
		ushort sampleFrames, SpheroDataStreamingMask sampleMask, byte sampleCount);
	[DllImport ("__Internal")]
	private static extern void enableControllerStreaming(ushort sampleRateDivisor,
		ushort sampleFrames, SpheroDataStreamingMask sampleMask);
	[DllImport ("__Internal")]
	private static extern void disableControllerStreaming();
}

#endif