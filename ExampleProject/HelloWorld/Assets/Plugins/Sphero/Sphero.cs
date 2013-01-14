using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class Sphero {
	
#if UNITY_ANDROID	
	private AndroidJavaObject m_AndroidJavaSphero;
	public AndroidJavaObject AndroidJavaSphero
    {
		get{ return this.m_AndroidJavaSphero; }
    }
	
	private AndroidJavaObject m_UnityBridge = (new AndroidJavaClass("orbotix.unity.UnityBridge")).CallStatic<AndroidJavaObject>("sharedBridge");
	
	// Cached Java Classes for efficient calls
	private AndroidJavaClass m_RGBLEDOutput = new AndroidJavaClass("orbotix.robot.base.RGBLEDOutputCommand");
#endif
	
	// Bluetooth Info Inner Data Structure Class
	public class BluetoothDeviceInfo {
		private string m_Name;
	    public string Name
	    {
			get{ return this.m_Name; }
			set{ this.m_Name = value; }
	    }
		private string m_UniqueId;
		public string UniqueId {
			get{ return this.m_UniqueId; }
			set{ this.m_UniqueId = value; }	
		}
		
		public BluetoothDeviceInfo(string name, string uniqueId) {
			m_Name = name;
			m_UniqueId = uniqueId;
		}
	}
	
	private BluetoothDeviceInfo m_DeviceInfo; 
	public BluetoothDeviceInfo DeviceInfo {
		get{ return this.m_DeviceInfo; }
	}
	
	// Current Sphero Color
	private int m_Color;
	public int Color 
	{
		get{ return this.m_Color; }
		set{ this.m_Color = value; }
	}
	
	// Connection State
	public enum Connection_State { Failed, Disconnected, Connecting, Connected };
	private Connection_State m_ConnectionState = Connection_State.Disconnected;
	public Connection_State ConnectionState
	{
		get{ return this.m_ConnectionState; }
		set{ this.m_ConnectionState = value; }
	}
	
#if UNITY_ANDROID	
	/*
	 * Default constructor used for Android 
	 */ 
	public Sphero(AndroidJavaObject sphero) {		
		m_AndroidJavaSphero = sphero;
	}
	
	/*
	 * More detailed constructor used for Android 
	 */ 
	public Sphero(AndroidJavaObject sphero, string bt_name, string bt_address) {		
		m_AndroidJavaSphero = sphero;
		m_DeviceInfo = new BluetoothDeviceInfo(bt_name, bt_address);
	}
#endif
	
	/*
	 * Default constructor used for iOS 
	 */ 
	public Sphero() {}
	
	/*
	 * Change Sphero's color to desired output
	 * @param red the amount of red from (0.0 - 1.0) intensity
	 * @param green the amount of green from (0.0 - 1.0) intensity
	 * @param blue the amount of blue from (0.0 - 1.0) intensity
	 */
	public void SetRGBLED(float red, float green, float blue) {
		#if UNITY_ANDROID	
			using( AndroidJavaClass jc = new AndroidJavaClass("orbotix.robot.base.RGBLEDOutputCommand") ) {
				jc.CallStatic("sendCommand",m_AndroidJavaSphero,red*255,green*255,blue*255);
			}
		#elif UNITY_IPHONE
			SpheroBridge.SetRGB(red,green,blue);
		#endif
		
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
	public void SetRGBLED(int color) {
		
		int red = (color & 0x00FF0000) >> 16;
		int green = (color & 0x0000FF00) >> 8;
		int blue = color & 0x000000FF;
		
		#if UNITY_ANDROID	
			m_RGBLEDOutput.CallStatic("sendCommand",m_AndroidJavaSphero,red,green,blue);
		#elif UNITY_IPHONE
			SpheroBridge.SetRGB(red/255,green/255,blue/255);
		#endif
		
		m_Color = color;
	}
	
	/*
	 * Get the current red color of the ball 
	 */ 
	public float GetRedIntensity() {
		return (m_Color & 0x00FF0000) >> 16;
	}
	
	/*
	 * Get the current green color of the ball 
	 */ 
	public float GetGreenIntensity() {
		return (m_Color & 0x0000FF00) >> 8;
	}
	
	/*
	 * Get the current blue color of the ball 
	 */ 
	public float GetBlueIntensity() {
		return m_Color & 0x000000FF;
	}
	
	/*
	 * Get the Name of the Sphero. (i.e. Sphero-PRR)
	 */
	public string GetName() {
		return m_DeviceInfo.Name;
	}
	
	/**
	 * Enable controller data streaming with infinite packets
     * @param divisor Divisor of the maximum sensor sampling rate (400 Hz)
     * @param packetFrames Number of samples created on the device before it sends a packet to the phone with samples
     * @param sensorMask Bitwise selector of data sources to stream
	 */
	public void EnableControllerStreaming(ushort divisor, ushort packetFrames, SpheroDataStreamingMask sensorMask) {		
		#if UNITY_ANDROID	
			m_UnityBridge.Call("enableControllerStreaming",m_AndroidJavaSphero,(int)divisor,(int)packetFrames,(long)sensorMask);
		#elif UNITY_IPHONE
			SpheroBridge.EnableControllerStreaming(divisor, packetFrames, sensorMask);
		#endif
	}
	
	/**
	 * Disable controller data streaming
	 */
	public void DisableControllerStreaming(ushort divisor, ushort packetFrames, SpheroDataStreamingMask sensorMask) {
		#if UNITY_ANDROID	
			m_UnityBridge.Call("disableControllerStreaming",m_AndroidJavaSphero);
		#elif UNITY_IPHONE
			SpheroBridge.DisableControllerStreaming();
		#endif
	}
	
	/**
	 * Start Streaming Data to Unity
     * @param divisor Divisor of the maximum sensor sampling rate (400 Hz)
     * @param packetFrames Number of samples created on the device before it sends a packet to the phone with samples
     * @param sensorMask Bitwise selector of data sources to stream
     * @param packetCount Packet count (set to 0 for unlimited streaming) 
	 */
	public void SetDataStreaming(ushort divisor, ushort packetFrames, SpheroDataStreamingMask sensorMask, ushort packetCount) {
		#if UNITY_ANDROID	
			m_UnityBridge.Call("setDataStreaming",m_AndroidJavaSphero, divisor, packetFrames, sensorMask, packetCount);
		#elif UNITY_IPHONE
			SpheroBridge.SetDataStreaming(divisor, packetFrames, sensorMask, (byte)packetCount);
		#endif
	}
}
