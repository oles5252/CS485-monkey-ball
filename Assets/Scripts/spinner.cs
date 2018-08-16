using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spinner : MonoBehaviour {

    public float rotationSpeed = 1f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Vector3 oldrot = transform.eulerAngles;
        oldrot.z -= rotationSpeed;
        transform.eulerAngles = oldrot;	
	}
}
