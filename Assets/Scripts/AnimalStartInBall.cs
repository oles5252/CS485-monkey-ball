using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalStartInBall : MonoBehaviour {

    public Transform spawnTransform;

	// Use this for initialization
	void Start () {
        transform.position = spawnTransform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
