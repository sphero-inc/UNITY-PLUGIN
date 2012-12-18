using UnityEngine;
using System.Collections;
using JsonFx.Json;
using System;

public class SpheroManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Debug.Log("setup robot connection");
		SpheroBridge.SetupRobotConnection();
	}
	
	// Update is called once per frame
	void Update () {
	}


}
