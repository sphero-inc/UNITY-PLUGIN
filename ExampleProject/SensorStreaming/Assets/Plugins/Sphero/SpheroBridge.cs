
using System.Collections;
using System.Runtime.InteropServices;

public class SpheroBridge {

	public static float s_calibrateGyroOffset = 0.0f;
	public static float s_calibrateOffset = 0.0f;
		
#if UNITY_ANDROID
	[DllImport ("RKUNBridge")]
#else
	[DllImport ("__Internal")]
#endif
	public static extern void SetupRobotConnection();
	
#if UNITY_ANDROID
	[DllImport ("RKUNBridge")]
#else
	[DllImport ("__Internal")]
#endif
	public static extern bool IsRobotConnected();
	
#if UNITY_ANDROID
	[DllImport ("RKUNBridge")]
#else
	[DllImport ("__Internal")]
#endif
	public static extern void SetRGB(float red, float green, float blue);
	
#if UNITY_ANDROID
	[DllImport ("RKUNBridge")]
#else
	[DllImport ("__Internal")]
#endif
	public static extern void Roll(int heading, float speed);
	
#if UNITY_ANDROID
	[DllImport ("RKUNBridge")]
#else
	[DllImport ("__Internal")]
#endif
	public static extern void SetHeading(int heading);
	
#if UNITY_ANDROID
	[DllImport ("RKUNBridge")]
#else
	[DllImport ("__Internal")]
#endif
	public static extern void SetBackLED(float intensity);
	
#if UNITY_ANDROID
	[DllImport ("RKUNBridge")]
#else
	[DllImport ("__Internal")]
#endif
	public static extern void SetDataStreaming(ushort sampleRateDivisor, 
		ushort sampleFrames, SpheroDataStreamingMask sampleMask, byte sampleCount);

#if UNITY_ANDROID
	[DllImport ("RKUNBridge")]
#else
	[DllImport ("__Internal")]
#endif
	public static extern void EnableControllerStreaming(ushort sampleRateDivisor,
		ushort sampleFrames, SpheroDataStreamingMask sampleMask);
		
#if UNITY_ANDROID
	[DllImport ("RKUNBridge")]
#else
	[DllImport ("__Internal")]
#endif
	public static extern void DisableControllerStreaming();
				
}
