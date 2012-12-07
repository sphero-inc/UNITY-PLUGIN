using System;
using SharpUnit;

namespace SharpUnit {
	public class SpheroDeviceSensorsAsyncDataTests : TestCase
	{
		
		[UnitTest]
		public void TestDecode()
		{
			string encodedString = "{\"class\":\"DeviceSensorsAsyncData\", " +
			"\"timeStamp\":123456, \"frameCount\":2, \"mask\":57344, " +
			"\"dataFrames\":[{\"class\":\"DeviceSensorsData\", " +
			"\"accelerometerData\":{\"class\":\"AccelerometerData\", \"normalized.x\":1.23, \"normalized.y\":1.23, \"normalized.z\":1.23}}, " +
			"{\"class\":\"DeviceSensorsData\"}]}";
			
			// Test that a message is created.
			SpheroDeviceMessage message = 
				SpheroDeviceMessageDecoder.messageFromEncodedString(encodedString);
			Assert.NotNull(message);
			Assert.True(message is SpheroDeviceSensorsAsyncData);
			Assert.Equal(123456, message.TimeStamp);
			
			// Specific test for SpheroDeviceSensorsAsyncData 
			SpheroDeviceSensorsAsyncData sensorsAsyncData = 
				(SpheroDeviceSensorsAsyncData)message;
				
			Assert.Equal(2, sensorsAsyncData.FrameCount);
			Assert.Equal(57344, sensorsAsyncData.Mask);		
			Assert.NotNull(sensorsAsyncData.Frames);
			Assert.True(sensorsAsyncData.Frames.Length > 1);
			
			// Check values for a DeviceSensorsData object
			SpheroDeviceSensorsData sensorsData = sensorsAsyncData.Frames[0];
			
			// Accelerometer
			Assert.Equal(1.23f, sensorsData.AccelerometerData.Normalized.X);
			Assert.Equal(1.23f, sensorsData.AccelerometerData.Normalized.Y);
			Assert.Equal(1.23f, sensorsData.AccelerometerData.Normalized.Z); 
		}
	}	
}