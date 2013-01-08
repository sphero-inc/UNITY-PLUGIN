using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HelloWorld : MonoBehaviour {
	
	// The color blue as an int with 100% opacity
	const int BLUE = unchecked((int)0xFF0000FF);
	const int BLACK = unchecked((int)0xFF000000);
	
	// Connected Sphero Robot
	List<Sphero> m_SpheroList;
	
	// Counter to determine if Sphero should have color or not
	int m_BlinkCounter;
	
	// Use this for initialization
	void Start () {
		// Get Connected Sphero
		m_SpheroList = SpheroProvider.GetSharedProvider().GetConnectedSpheros();
	}
	
	// Update is called once per frame
	void Update () {
		m_BlinkCounter++;
		if( m_BlinkCounter % 20 == 0 ) {
			foreach( Sphero sphero in m_SpheroList ) {
				// Set the Sphero color to blue 
				if( sphero.Color == BLACK ) {
					sphero.SetRGBLED(BLUE);
				}
				else {
					sphero.SetRGBLED(BLACK);	
				}
			}
		}
	}
	
	void OnApplicationPause() {
		// Disconnect robots
		SpheroProvider.GetSharedProvider().DisconnectSpheros();
	}
}
