using System;
using SharpUnit;

namespace SharpUnit {
	public class SpheroDeviceSensorsAsyncDataTests : TestCase
	{
		[UnitTest]
		public void TestDecode()
		{
			string encodedString = "{\"class\":\"DeviceSensorsAsyncData\", " +
			"\"timeStamp\":123456, \"frameCount\":3, \"mask\":1 }";
			SpheroDeviceMessage message = 
				SpheroDeviceMessageDecoder.messageFromEncodedString(encodedString);
			Assert.NotNull(message);
			Assert.True(message is SpheroDeviceSensorsAsyncData);
			Assert.Equal(123456, message.TimeStamp);
			
			SpheroDeviceSensorsAsyncData sensorsAsyncData = 
				(SpheroDeviceSensorsAsyncData)message;
			Assert.Equal(3, sensorsAsyncData.FrameCount);
			Assert.Equal(1, sensorsAsyncData.Mask);		
		}
	}	
}