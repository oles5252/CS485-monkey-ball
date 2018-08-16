using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TryStanding : MonoBehaviour {

    public Vector3 standingRotation = new Vector3(0, 0, 0); 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, standingRotation, 0.75f);
	}
}
