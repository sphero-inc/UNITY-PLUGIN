using UnityEngine;
using System.Collections;

public class HelloWorld : MonoBehaviour {
	
	// The color blue as an int with 100% opacity
	const int BLUE = unchecked((int)0xFF0000FF);
	const int BLACK = unchecked((int)0xFF000000);
	
	// Connected Sphero Robot
	Sphero m_Sphero;
	
	// Counter to determine if Sphero should have color or not
	int m_BlinkCounter;
	
	// Use this for initialization
	void Start () {
		// Get Connected Sphero
		m_Sphero = SpheroProvider.getSharedProvider().getConnectedSpheros()[0];
	}
	
	// Update is called once per frame
	void Update () {
		m_BlinkCounter++;
		if( m_BlinkCounter % 20 == 0 ) {
			// Set the Sphero color to blue 
			if( m_Sphero.getColor() == BLACK ) {
				m_Sphero.setRGB(BLUE);
			}
			else {
				m_Sphero.setRGB(BLACK);	
			}
		}
	}
	
	void OnApplicationPause() {
		// Disconnect robots
		SpheroProvider.getSharedProvider().disconnectSpheros();
	}
}
