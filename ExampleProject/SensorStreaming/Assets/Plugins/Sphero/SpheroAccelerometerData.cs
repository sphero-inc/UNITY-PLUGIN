using System;

public class SpheroAccelerometerData : SpheroSensorData
{
	public class Acceleration {
		public float X { get; set; }
		public float Y { get; set; }
		public float Z { get; set; }
	}
	
	private Acceleration normalized = new Acceleration();
	
	public Acceleration Normalized 
	{
		get{ return normalized; }
	}
	
	public SpheroAccelerometerData(SpheroDeviceMessageDecoder decoder) : base(decoder)
	{
		normalized.X = decoder.DecodeFloat("normalized.x");
		normalized.Y = decoder.DecodeFloat("normalized.y");
		normalized.Z = decoder.DecodeFloat("normalized.z");
	}
}