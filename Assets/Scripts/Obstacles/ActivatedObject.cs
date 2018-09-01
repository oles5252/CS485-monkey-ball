using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatedObject : MonoBehaviour {

    bool openDoor;

    bool closeDoor;

    bool lowerBridge;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (openDoor)
        {
        
        }
        if (lowerBridge)
        {
            //lower the bridge
        }
	}

    void startOpenDoor()
    {
        openDoor = true;
    }
}
