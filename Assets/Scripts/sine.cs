using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sine : MonoBehaviour {

    public float xForWave; //should be between -pi/2 and pi/2

    public float maxAmplitude = 10;

    public float minAmplitude = -10;

    public bool flip;

    private float currAmplitude;

    private float initialXPos;

    public float speed = 3f;

	// Use this for initialization
	void Start () {
        currAmplitude = 0;
        initialXPos = transform.position.x;
    }
	
	// Update is called once per frame
	void Update () {
        if(currAmplitude >= maxAmplitude-0.01f & !flip)
        {
            flip = true;
        }
        else if(currAmplitude <= minAmplitude+0.01f & flip)
        {
            flip = false;
        }
        if (!flip)
        {
            currAmplitude += speed*Time.deltaTime;
        }
        else
        {
            currAmplitude -= speed*Time.deltaTime;
        }
        Vector3 position = transform.position;
        position.x = initialXPos + (Mathf.Cos(xForWave) * currAmplitude);
        transform.position = position;
	}
}
