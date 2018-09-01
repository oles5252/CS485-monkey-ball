using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class Timer : MonoBehaviour {

    Text time;

    float timer;

	// Use this for initialization
	void Start () {
        time = GetComponent<Text>();
        timer = PlayerPrefs.GetFloat("currentTime", 0.0f);
        StartCoroutine(Count());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator Count()
    {
        while (true)
        {
            timer += Time.deltaTime;
            float min = Mathf.Floor(timer / 60);
            float sec = Mathf.Floor(timer - min * 60);
            time.text = "Timer: " + min + "m " + sec + "s";
            yield return null;
        }
    }

    private void OnDestroy()
    {
        PlayerPrefs.SetFloat("currentTime",timer);
    }
}
