using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepInsideTrigger : MonoBehaviour {

    public Transform catSpawnPoint;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "keepInside")
        {
            transform.position = catSpawnPoint.position;
        }
    }
}
