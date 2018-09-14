using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadScore : MonoBehaviour {

    private float timer;

    // Use this for initialization
    void Start () {
        timer = PlayerPrefs.GetFloat("currentTime", 0.0f);
    }
	
	// Update is called once per frame
	void Update () {
        float min = Mathf.Floor(timer / 60);
        float sec = Mathf.Floor(timer - min * 60);
        GetComponent<Text>().text = "New Score: " + min + "m " + sec + "s";
	}
}
