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
	
	/* Sphero's RGB LED color */
	protected Color m_RGBLEDColor;
	public Color RGBLEDColor {
		get{ return this.m_RGBLEDColor; }
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
	 * Change Sphero's color to desired output
	 * @param red the amount of red from (0.0 - 1.0) intensity
	 * @param green the amount of green from (0.0 - 1.0) intensity
	 * @param blue the amount of blue from (0.0 - 1.0) intensity
	 */
	abstract public void SetRGBLED(float red, float green, float blue);
	/**
	 * Start Streaming Data to Unity
     * @param divisor Divisor of the maximum sensor sampling rate (400 Hz)
     * @param packetFrames Number of samples created on the device before it sends a packet to the phone with samples
     * @param sensorMask Bitwise selector of data sources to stream
     * @param packetCount Packet count (set to 0 for unlimited streaming) 
	 */
	abstract public void SetDataStreaming(ushort divisor, ushort packetFrames, SpheroDataStreamingMask sensorMask, ushort packetCount);
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
	/* 
	 * Roll Sphero in a direction at a certain speed 
	 * @param angle (in degrees 0-359) of the roll direction (0 is y-axis, 90 is x-axis)
	 * @param speed 0.0f is stopped, 1.0f is full speed
	 */
	abstract public void Roll(int heading, float speed);
	/* 
	 * Set the current heading of Sphero to the heading in the parameter
	 * @parma new heading to set to the current heading of Sphero
	 */
	abstract public void SetHeading(int heading);
	/*
	 * Set the blue back LED on Sphero to a brightness intensity
	 * @param intensity is the brightness of the back LED
	 */
	abstract public void SetBackLED(float intensity);

	abstract public void GetPowerState();
}
