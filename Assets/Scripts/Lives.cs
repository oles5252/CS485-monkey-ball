using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class Lives : MonoBehaviour {

    Text life;

    int lives;

    // Use this for initialization
    void Start()
    {
        life = GetComponent<Text>();
        lives = PlayerPrefs.GetInt("currentLives", 3);
        life.text = "Lives: " + lives;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
