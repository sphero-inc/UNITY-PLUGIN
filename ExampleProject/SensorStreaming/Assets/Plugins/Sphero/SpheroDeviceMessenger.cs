using System;
using System.Collections.Generic;
using JsonFx.Json;
using System.Runtime.InteropServices;


public class SpheroDeviceMessenger  {
	public event EventHandler ResponseDataReceived;
	public event EventHandler AsyncDataReceived;

	private static SpheroDeviceMessenger sharedInstance;

	private delegate void ReceiveDeviceMessageDelegate(string encodedMessage);

	static SpheroDeviceMessenger() 
	{
		_RegisterRecieveDeviceMessageCallback(ReceiveDeviceMessage);
	}

	private SpheroDeviceMessenger()
	{
		sharedInstance = this;
	}

	protected virtual void OnResponsDataReceived(EventArgs eventArgs)
	{
		EventHandler handler = ResponseDataReceived;
		if (handler != null) {
			handler(this, eventArgs);
		}
	}

	protected virtual void OnAsyncDataReceived(EventArgs eventArgs)
	{
		EventHandler handler = AsyncDataReceived;
		if (handler != null) {
			handler(this, eventArgs);
		}
	}

	[MonoPInvokeCallback (typeof (ReceiveDeviceMessageDelegate))]
	protected static void ReceiveDeviceMessage(string encodedMessage) 
	{
		// Decode the stirng into an object
		// Pass it on to event handlers
	}

	[DllImport ("__Internal")]
	private static extern void _RegisterRecieveDeviceMessageCallback(ReceiveDeviceMessageDelegate callback);
}
