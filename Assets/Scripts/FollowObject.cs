using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour {

    public GameObject followed;

    public Vector3 offset;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        gameObject.transform.position = new Vector3(followed.transform.position.x + offset.x, followed.transform.position.y + offset.y, followed.transform.position.z + offset.z);
	}
}
