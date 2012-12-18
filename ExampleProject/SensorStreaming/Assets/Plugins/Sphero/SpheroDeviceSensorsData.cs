using System;

public class SpheroDeviceSensorsData {
	private SpheroAccelerometerData accelerometerData;
	private SpheroAttitudeData		attitudeData;
	private SpheroQuaternionData	quaternionData;
	private SpheroBackEMFData		backEMFData;
	
	
	public SpheroAccelerometerData AccelerometerData
	{
		get{ return accelerometerData; }
	}
	
	public SpheroAttitudeData AttitudeData
	{
		get{ return attitudeData; }
	}
	
	public SpheroQuaternionData QuaternionData
	{
		get{ return quaternionData; }
	}
	
	public SpheroBackEMFData BackEMFData
	{
		get{ return backEMFData; }
	}
	
	public SpheroDeviceSensorsData(SpheroDeviceMessageDecoder decoder)
	{
		accelerometerData = 
			(SpheroAccelerometerData)decoder.DecodeObject("accelerometerData");
		attitudeData = (SpheroAttitudeData)decoder.DecodeObject("attitudeData");
		quaternionData = (SpheroQuaternionData)decoder.DecodeObject("quaternionData");
		backEMFData = (SpheroBackEMFData)decoder.DecodeObject("backEMFData");
	}
}