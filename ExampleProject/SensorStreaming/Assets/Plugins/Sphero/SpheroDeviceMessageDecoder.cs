using System;
using System.Collections.Generic;
using JsonFx.Json;

public class SpheroDeviceMessageDecoder {
	Dictionary<string, object> dictionaryRepresentation = null;
	
	public static SpheroDeviceMessage messageFromEncodedString(string encodedMessage)
	{
		SpheroDeviceMessageDecoder decoder = 
			new SpheroDeviceMessageDecoder(encodedMessage);
		// Get the class from the decoder to make an object. "Sphero" is added
		// for namespacing.
		string className = "Sphero" + (string)decoder.DecodeObject("class");
		
		// Create an instance from this class name
		Type messageType = Type.GetType(className);
		Object[] parameters = new Object[] {decoder};
		object message = Activator.CreateInstance(messageType, parameters);
		
		return (SpheroDeviceMessage)message;
	}

	public SpheroDeviceMessageDecoder(string encodedMessage) 
	{
		JsonReader jsonReader = new JsonReader(encodedMessage);
		dictionaryRepresentation = jsonReader.Deserialize< Dictionary<string,object> >();
	}	
	
	public object DecodeObject(string key)
	{
		object value = null;
		dictionaryRepresentation.TryGetValue(key, out value);
		return value;
	}
		
	public object DecodeString(string key)
	{
		return (string)DecodeObject(key);
	}
	
	public int DecodeInt32(string key)
	{
		return Convert.ToInt32(DecodeObject(key));
	}
	
	public long DecodeInt64(string key)
	{
		return Convert.ToInt64(DecodeObject(key));
	}
	
	public ulong DecodeUInt64(string key)
	{
		return Convert.ToUInt64(DecodeObject(key));
	}
}