using System;

public abstract class SpheroDeviceMessage {
	private long timeStamp;

	public long TimeStamp { get {return timeStamp;} }

	public SpheroDeviceMessage() {
		timeStamp = DateTime.Now.Ticks;
	}

	public SpheroDeviceMessage(SpheroDeviceMessageDecoder decoder) {
		timeStamp = decoder.DecodeInt64("timeStamp");	
	}
}