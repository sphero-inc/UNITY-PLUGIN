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

		SpheroBridge.SetRGB(red,green,blue);
		
		// Set the alpha to 1
		m_Color = 255;
		m_Color = m_Color << 8;
		// Set red bit and shift 8 left
		m_Color += (int)(255 * red);
		m_Color = m_Color << 8;
		// Set green bit and shift 8 left
		m_Color += (int)(255 * green);
		m_Color = m_Color << 8;
		// Set blue bit
		m_Color += (int)(255 * blue);
	}
	
	/*
	 * Change Sphero's color to desired output
	 * @param color is a hexadecimal representation of color
	 */
	override public void SetRGBLED(int color) {
		
		int red = (color & 0x00FF0000) >> 16;
		int green = (color & 0x0000FF00) >> 8;
		int blue = color & 0x000000FF;
		
		SpheroBridge.SetRGB(red/255,green/255,blue/255);
		
		m_Color = color;
	}
	
	/**
	 * Enable controller data streaming with infinite packets
     * @param divisor Divisor of the maximum sensor sampling rate (400 Hz)
     * @param packetFrames Number of samples created on the device before it sends a packet to the phone with samples
     * @param sensorMask Bitwise selector of data sources to stream
	 */
	override public void EnableControllerStreaming(ushort divisor, ushort packetFrames, SpheroDataStreamingMask sensorMask) {		
		SpheroBridge.EnableControllerStreaming(divisor, packetFrames, sensorMask);
	}
	
	/**
	 * Disable controller data streaming
	 */
	override public void DisableControllerStreaming() {
			SpheroBridge.DisableControllerStreaming();
	}
	
	/**
	 * Start Streaming Data to Unity
     * @param divisor Divisor of the maximum sensor sampling rate (400 Hz)
     * @param packetFrames Number of samples created on the device before it sends a packet to the phone with samples
     * @param sensorMask Bitwise selector of data sources to stream
     * @param packetCount Packet count (set to 0 for unlimited streaming) 
	 */
	override public void SetDataStreaming(ushort divisor, ushort packetFrames, SpheroDataStreamingMask sensorMask, ushort packetCount) {
		SpheroBridge.SetDataStreaming(divisor, packetFrames, sensorMask, (byte)packetCount);
	}
}

#endif