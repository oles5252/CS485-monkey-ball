using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour {

    public SceneHandler handler;

	// Use this for initialization
	void Start () {
        handler = FindObjectOfType<SceneHandler>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            handler.loadNextLevel();
        }
    }
}
