using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public abstract class Sphero {
	
	/* Bluetooth Info Inner Data Structure Class */
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
	/* Bluetooth Device Member Variable */
	protected BluetoothDeviceInfo m_DeviceInfo; 
	public BluetoothDeviceInfo DeviceInfo {
		get{ return this.m_DeviceInfo; }
	}
	
	/* Current Sphero Color */
	protected int m_Color;
	public int Color 
	{
		get{ return this.m_Color; }
		set{ this.m_Color = value; }
	}
	
	/* Connection State */
	public enum Connection_State { Failed, Disconnected, Connecting, Connected };
	protected Connection_State m_ConnectionState = Connection_State.Disconnected;
	public Connection_State ConnectionState
	{
		get{ return this.m_ConnectionState; }
		set{ this.m_ConnectionState = value; }
	}
	
	/*
	 * Default constructor used for iOS 
	 */ 
	public Sphero() {}
	
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
	 * Change Sphero's color to desired output
	 * @param red the amount of red from (0.0 - 1.0) intensity
	 * @param green the amount of green from (0.0 - 1.0) intensity
	 * @param blue the amount of blue from (0.0 - 1.0) intensity
	 */
	abstract public void SetRGBLED(float red, float green, float blue);
	/*
	 * Change Sphero's color to desired output
	 * @param color is a hexadecimal representation of color
	 */
	abstract public void SetRGBLED(int color);
	/**
	 * Enable controller data streaming with infinite packets
     * @param divisor Divisor of the maximum sensor sampling rate (400 Hz)
     * @param packetFrames Number of samples created on the device before it sends a packet to the phone with samples
     * @param sensorMask Bitwise selector of data sources to stream
	 */
	abstract public void EnableControllerStreaming(ushort divisor, ushort packetFrames, SpheroDataStreamingMask sensorMask);
	/**
	 * Disable controller data streaming
	 */
	abstract public void DisableControllerStreaming();
	/**
	 * Start Streaming Data to Unity
     * @param divisor Divisor of the maximum sensor sampling rate (400 Hz)
     * @param packetFrames Number of samples created on the device before it sends a packet to the phone with samples
     * @param sensorMask Bitwise selector of data sources to stream
     * @param packetCount Packet count (set to 0 for unlimited streaming) 
	 */
	abstract public void SetDataStreaming(ushort divisor, ushort packetFrames, SpheroDataStreamingMask sensorMask, ushort packetCount);
}
