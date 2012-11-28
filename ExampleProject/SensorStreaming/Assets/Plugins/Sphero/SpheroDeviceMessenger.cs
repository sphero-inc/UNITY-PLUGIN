using System.Collections;
using System;
using JsonFx.Json;
using System.Runtime.InteropServices;


public class SpheroDeviceMessenger  {
	public event EventHandler ResponseDataReceived;
	public event EventHandler AsyncDataReceived;

	private delegate void ReceiveDeviceMessageDelegate(string encodedMessage);

	static SpheroDeviceMessenger() 
	{
		_RegisterRecieveDeviceMessageCallback(ReceiveDeviceMessage);
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
		// Decode the stirng into an object.
		Console.WriteLine(encodedMessage);
		JsonReader reader = new JsonReader(encodedMessage);
		object deserializedObject = reader.Deserialize();
		Type type = deserializedObject.GetType();
		Console.WriteLine(type);
	}

	[DllImport ("__Internal")]
	private static extern void _RegisterRecieveDeviceMessageCallback(ReceiveDeviceMessageDelegate callback);
}
