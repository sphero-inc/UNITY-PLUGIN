using UnityEngine;
using System.Collections;
using JsonFx.Json;
using System;

public class SpheroManager : MonoBehaviour {
	private bool streaming = false;
	private SpheroDeviceMessenger deviceMessenger;

	// Use this for initialization
	void Start () {
		Debug.Log("setup robot connection");
		SpheroBridge._RKUNSetupRobotConnection();
		deviceMessenger = new SpheroDeviceMessenger();
	}
	
	// Update is called once per frame
	void Update () {
		if (SpheroBridge._RKUNIsRobotConnected() && !streaming) {
			Debug.Log("starting data streaming");
			SpheroBridge._enableDataStreaming();
			streaming = true;
		}
	}

}
