using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour {

    public GameObject player;

    public GameObject camera;

    Vector3 offset = new Vector3(0, 2, -7);

	// Use this for initialization
	void Start () {
        player.transform.position = gameObject.transform.position;
        camera.transform.position = gameObject.transform.position + offset;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
