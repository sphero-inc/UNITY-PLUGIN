using System;
using System.Collections.Generic;
using JsonFx.Json;
using System.Runtime.InteropServices;


public class SpheroDeviceMessenger  {
	
	public event MessengerEventHandler ResponseDataReceived;
	public event MessengerEventHandler AsyncDataReceived;

	public delegate void MessengerEventHandler(object sender,  MessengerEventArgs eventArgs);

	public class MessengerEventArgs : EventArgs
	{
		private SpheroDeviceMessage message;
		
		public SpheroDeviceMessage Message { get{ return message; } }
		
		public MessengerEventArgs( SpheroDeviceMessage message )
		{
			this.message = message;
		}
	}
	
	private static SpheroDeviceMessenger sharedInstance;
	public static SpheroDeviceMessenger SharedInstance { get{ return sharedInstance; } }
	
	private delegate void ReceiveDeviceMessageDelegate(string encodedMessage);

	static SpheroDeviceMessenger() 
	{
		UnityEngine.Debug.Log("Instance Created");
		sharedInstance = new SpheroDeviceMessenger();
		_RegisterRecieveDeviceMessageCallback(ReceiveDeviceMessage);
	}

	private SpheroDeviceMessenger()
	{
	}

	protected virtual void OnResponseMessageReceived(MessengerEventArgs eventArgs)
	{
		MessengerEventHandler handler = ResponseDataReceived;
		if (handler != null) {
			handler(this, eventArgs);
		}
	}

	protected virtual void OnAsyncMessageReceived(MessengerEventArgs eventArgs)
	{
		MessengerEventHandler handler = AsyncDataReceived;
		if (handler != null) {
			handler(this, eventArgs);
		}
	}

	[MonoPInvokeCallback (typeof (ReceiveDeviceMessageDelegate))]
	protected static void ReceiveDeviceMessage(string encodedMessage) 
	{
		UnityEngine.Debug.Log(encodedMessage);
		// Decode the stirng into an object
		SpheroDeviceMessage message = SpheroDeviceMessageDecoder.messageFromEncodedString(encodedMessage);
		
		// Pass it on to event handlers
		if ( message is SpheroDeviceAsyncMessage) {
			sharedInstance.OnAsyncMessageReceived(new MessengerEventArgs(message));
		}
	}
	
#if UNITY_ANDROID
	[DllImport ("unity_bridge")]
#else
	[DllImport ("__Internal")]
#endif
	private static extern void _RegisterRecieveDeviceMessageCallback(ReceiveDeviceMessageDelegate callback);
}
