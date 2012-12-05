using System;

public class SpheroDeviceSensorsAsyncData : SpheroDeviceAsyncMessage
{
	private int frameCount;
	private ulong mask;
	
	public int FrameCount { get{ return frameCount; } }
	public ulong Mask { get{ return mask; } }	   
	
	public SpheroDeviceSensorsAsyncData(SpheroDeviceMessageDecoder decoder) 
		: base(decoder)
	{
		frameCount = decoder.DecodeInt32("frameCount");
		mask = decoder.DecodeUInt64("mask");
	}
	
}