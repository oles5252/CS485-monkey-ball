using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeFOV : MonoBehaviour {

    public int fov;

	// Use this for initialization
	void Start () {
        fov = PlayerPrefs.GetInt("fov", 60);
        GetComponent<Camera>().fieldOfView = fov;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
