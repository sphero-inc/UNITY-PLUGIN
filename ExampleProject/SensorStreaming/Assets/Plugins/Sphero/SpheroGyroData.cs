using System;

public class SpheroGyroData : SpheroSensorData {
	private ThreeAxisSensor rotationRate;
	private ThreeAxisSensor rotationRateRaw;
	
	public ThreeAxisSensor RotationRate { get{ return rotationRate; } }
	public ThreeAxisSensor RotationRateRaw { get{ return rotationRateRaw; } }
	
	public SpheroGyroData( SpheroDeviceMessageDecoder decoder ) : base(decoder)
	{
		rotationRate.x = decoder.DecodeUInt16("rotationRate.x");
		rotationRate.y = decoder.DecodeUInt16("rotationRate.y");
		rotationRate.z = decoder.DecodeUInt16("rotationRate.z");
		rotationRateRaw.x = decoder.DecodeUInt16("rotationRateRaw.x");
		rotationRateRaw.y = decoder.DecodeUInt16("rotationRateRaw.y");
		rotationRateRaw.z = decoder.DecodeUInt16("rotationRateRaw.z");
	}
}