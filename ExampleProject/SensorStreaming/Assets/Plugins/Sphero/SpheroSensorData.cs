using System;

public class SpheroSensorData {

	public struct ThreeAxisSensor {
		public ushort x;
		public ushort y;
		public ushort z;
	}
	
	private long timeStamp;
	
	public long TimeStamp
	{
		get{ return timeStamp; }
	}
	
	public SpheroSensorData(SpheroDeviceMessageDecoder decoder)
	{
		
	}
}