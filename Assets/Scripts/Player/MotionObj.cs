using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionObj : MonoBehaviour {

    public Camera camera;

    public GameObject player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.rotation = new Quaternion(transform.rotation.x-camera.transform.rotation.x,
                                                transform.rotation.y - camera.transform.rotation.y,
                                                    transform.rotation.z - camera.transform.rotation.z,
                                                        transform.rotation.w - camera.transform.rotation.w);
        transform.LookAt(player.transform);
	}
}
