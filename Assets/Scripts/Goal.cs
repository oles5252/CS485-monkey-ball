using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour {

    public SceneHandler handler;
    public bool isFinalLevel = false;

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
            if (!isFinalLevel)
            {
                handler.loadNextLevel();
            }
            else
            {
                SceneManager.LoadScene("Leaderboard");
            }
        }
    }
}
