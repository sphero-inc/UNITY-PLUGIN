using System;

public class SpheroDeviceSensorsData {
	private SpheroAccelerometerData accelerometerData;
	
	public SpheroAccelerometerData AccelerometerData
	{
		get{ return accelerometerData; }
	}
	
	public SpheroDeviceSensorsData(SpheroDeviceMessageDecoder decoder)
	{
		accelerometerData = (SpheroAccelerometerData)decoder.DecodeObject("accelerometerData");
	}
}