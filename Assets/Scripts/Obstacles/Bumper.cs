using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : MonoBehaviour {

    public float force = 10;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        // get angle
        Vector3 dir = collision.contacts[0].point - collision.gameObject.transform.position;
        // Get opposite vector and normalize
        dir = -dir.normalized;
        // And finally we add force in the direction of dir and multiply it by force. 
        // This will push back the player
        collision.gameObject.GetComponent<Rigidbody>().AddForce(dir * force, ForceMode.Impulse);
    }
}
