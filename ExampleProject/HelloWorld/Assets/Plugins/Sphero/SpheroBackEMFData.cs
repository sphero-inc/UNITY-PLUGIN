using System;

public class SpheroBackEMFData : SpheroSensorData {
	public struct Motors {
		public ushort right;
		public ushort left;		
	}
	
	private Motors raw;
	private Motors filtered;
	
	public Motors Filtered { get{ return filtered; } }
	public Motors Raw { get{ return raw; } }
	
	public SpheroBackEMFData(SpheroDeviceMessageDecoder decoder) : base(decoder)
	{
		filtered.right = decoder.DecodeUInt16("filtered.rightMotor");
		filtered.left = decoder.DecodeUInt16("filtered.leftMotor");
		raw.right = decoder.DecodeUInt16("raw.rightMotor");
		raw.left = decoder.DecodeUInt16("raw.leftMotor");
	}

}