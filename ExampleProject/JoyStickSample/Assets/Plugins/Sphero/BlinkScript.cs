using UnityEngine;
using System.Collections;

public class BlinkScript : MonoBehaviour {
	
	private int counter = 0;
	
	// Use this for initialization
	void Start () {
		RKUNBridge._RKUNSetupRobotConnection();
	}
	
	// Update is called once per frame
	void Update () {
		//Make Sphero change color every 30 frames
		if(counter == 0) {
			RKUNBridge._RKUNRGB(1.0f, 0.0f, 0.0f);	
		} else if(counter == 30) {
			RKUNBridge._RKUNRGB(0.0f, 1.0f, 0.0f);	
		} else if(counter == 60) {
			RKUNBridge._RKUNRGB(0.0f, 0.0f, 1.0f);	
		}
		counter++;
		if(counter==90) counter = 0;
	}
}
