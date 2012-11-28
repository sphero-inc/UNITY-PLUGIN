
using System.Collections;
using System.Runtime.InteropServices;

public class RKUNBridge {

	public static float s_calibrateGyroOffset = 0.0f;
	public static float s_calibrateOffset = 0.0f;
	
	[StructLayout(LayoutKind.Sequential)]
	public struct RKUNData {
        public float pitch;
        public float roll;
        public float yaw;
        public float x;
        public float y;
        public float z;
    }
	
#if UNITY_ANDROID
	[DllImport ("RKUNBridge")]
#else
	[DllImport ("__Internal")]
#endif
	public static extern void _RKUNSetupRobotConnection();
	
#if UNITY_ANDROID
	[DllImport ("RKUNBridge")]
#else
	[DllImport ("__Internal")]
#endif
	public static extern bool _RKUNIsRobotConnected();
	
#if UNITY_ANDROID
	[DllImport ("RKUNBridge")]
#else
	[DllImport ("__Internal")]
#endif
	public static extern void _RKUNRGB(float red, float green, float blue);
	
#if UNITY_ANDROID
	[DllImport ("RKUNBridge")]
#else
	[DllImport ("__Internal")]
#endif
	public static extern void _RKUNRoll(int heading, float speed);
	
#if UNITY_ANDROID
	[DllImport ("RKUNBridge")]
#else
	[DllImport ("__Internal")]
#endif
	public static extern void _RKUNCalibrate(int heading);
	
#if UNITY_ANDROID
	[DllImport ("RKUNBridge")]
#else
	[DllImport ("__Internal")]
#endif
	public static extern void _RKUNBackLED(float intensity);
	
#if UNITY_ANDROID
	[DllImport ("RKUNBridge")]
#else
	[DllImport ("__Internal")]
#endif
	public static extern void _enableDataStreaming();
	
#if UNITY_ANDROID
	[DllImport ("RKUNBridge")]
#else
	[DllImport ("__Internal")]
#endif
	public static extern void _disableDataStreaming();
	
	
#if UNITY_ANDROID
	[DllImport ("RKUNBridge")]
#else
	[DllImport ("__Internal")]
#endif
	public static extern RKUNData _RKUNSensorData();
	
}
