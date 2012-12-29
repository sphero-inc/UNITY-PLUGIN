using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class Sphero {
	
// How do we manage multiple robots from here?	
#if UNITY_ANDROID	
	AndroidJavaObject m_JavaSphero;
#endif
	
	int m_Color;
	
	/*
	 * Default constructor used for Android 
	 */ 
	public Sphero(AndroidJavaObject sphero) {
		#if UNITY_ANDROID			
			m_JavaSphero = sphero;
		#endif
	}
	
	/*
	 * Default constructor used for iOS 
	 */ 
	public Sphero() {		
		m_JavaSphero = null;
	}
	
	/*
	 * Change Sphero's color to desired output
	 * @param red the amount of red from (0.0 - 1.0) intensity
	 * @param green the amount of green from (0.0 - 1.0) intensity
	 * @param blue the amount of blue from (0.0 - 1.0) intensity
	 */
	public void setRGB(float red, float green, float blue) {
		#if UNITY_ANDROID	
			using( AndroidJavaClass jc = new AndroidJavaClass("orbotix.robot.base.RGBLEDOutputCommand") ) {
				jc.CallStatic("sendCommand",m_JavaSphero,red*255,green*255,blue*255);
			}
		#elif UNITY_IPHONE
			SpheroBridge.SetRGB(red,green,blue);
		#endif
		
		// Set the alpha to 1
		m_Color = 255;
		m_Color = m_Color << 8;
		// Set red bit and shift 8 left
		m_Color += (int)(255 * red);
		m_Color = m_Color << 8;
		// Set green bit and shift 8 left
		m_Color += (int)(255 * green);
		m_Color = m_Color << 8;
		// Set blue bit
		m_Color += (int)(255 * blue);
	}
	
		/*
	 * Change Sphero's color to desired output
	 * @param color is a hexadecimal representation of color
	 */
	public void setRGB(int color) {
		
		int red = (color & 0x00FF0000) >> 16;
		int green = (color & 0x0000FF00) >> 8;
		int blue = color & 0x000000FF;
		
		#if UNITY_ANDROID	
			using( AndroidJavaClass jc = new AndroidJavaClass("orbotix.robot.base.RGBLEDOutputCommand") ) {
				jc.CallStatic("sendCommand",m_JavaSphero,red,green,blue);
			}
		#elif UNITY_IPHONE
			SpheroBridge.SetRGB(red/255,green/255,blue/255);
		#endif
		
		m_Color = color;
	}
	
	/*
	 * Get the current color of the ball as an int with an alpha channel of 255
	 */ 
	public int getColor() {
		return m_Color;
	}
	
	/*
	 * Get the current red color of the ball 
	 */ 
	public float getRedIntensity() {
		return (m_Color & 0x00FF0000) >> 16;
	}
	
	/*
	 * Get the current green color of the ball 
	 */ 
	public float getGreenIntensity() {
		return (m_Color & 0x0000FF00) >> 8;
	}
	
	/*
	 * Get the current blue color of the ball 
	 */ 
	public float getBlueIntensity() {
		return m_Color & 0x000000FF;
	}
}
