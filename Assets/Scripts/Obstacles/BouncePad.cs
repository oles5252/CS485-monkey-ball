using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePad : MonoBehaviour {

    public float forceScale = 100;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        //add force to the player in the up vector
        collision.gameObject.GetComponent<Rigidbody>().AddForce(gameObject.transform.up * forceScale, ForceMode.Impulse);
    }
}
