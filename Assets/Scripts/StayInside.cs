using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayInside : MonoBehaviour {

    Rigidbody rigidbody;

    bool alreadyFlipped = false;

    Collider currCollider;

	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "keepInside")
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit))
            {
                rigidbody.velocity = Vector3.Reflect(rigidbody.velocity * 2, hit.normal);
                print("Bounced!");
            }
        }
    }
}
