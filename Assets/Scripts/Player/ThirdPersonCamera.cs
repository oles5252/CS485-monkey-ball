using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Camera))]

public class ThirdPersonCamera : MonoBehaviour {

    public GameObject player;

    private Vector3 positionOffset;

    public Vector3 defaultOffset = new Vector3(0, 3, -9);

    float turnSpeed = 7f; //set default at 5, but this should come from playerMovement

    float defaultFOV = 60;

    float forwardFOV = 75;

    float backwardFOV = 45;

    float defaultXAngle = 2;

    float forwardXAngle = 12;

    float backwardXAngle = -8;

    float rightRotationOffset = -0.3f;

    float leftRotationOffset = 0.3f;

    float xRotationOffset;

    float zRotationOffset;

    Vector3 lastDirection = new Vector3(0,0,1);

    public int currentFlipState = 0;

    enum flipStates
    {
        stopped, forward, backward
    }

	// Use this for initialization
	void Start () {
        positionOffset = defaultOffset;
    }

    // Update is called once per frame
    void Update () {
        
    }

    private void LateUpdate()
    {
        //player = GameObject.FindGameObjectsWithTag("Player")[0];
        //camera = gameObject.GetComponent<Camera>();

        /*
         if (Input.GetKey(KeyCode.W))
         {
             float currFOV = camera.fieldOfView;
             camera.fieldOfView = Mathf.Lerp(currFOV, forwardFOV, 0.05f);
             Vector3 currAngle = transform.rotation.eulerAngles;
             //zRotationOffset = Mathf.Lerp(zRotationOffset, forwardXAngle, 0.05f), currAngle.y, currAngle.z);
             Vector3 currPos = player.transform.position;
         }
         else if (Input.GetKey(KeyCode.S))
         {
             float currFOV = camera.fieldOfView;
             camera.fieldOfView = Mathf.Lerp(currFOV, backwardFOV, 0.05f);
             Vector3 currAngle = transform.rotation.eulerAngles;
             //transform.rotation.eulerAngles.Set(Mathf.Lerp(currAngle.x, backwardXAngle, 0.05f), currAngle.y, currAngle.z);
           //  print(positionOffset.z);
             /*
             if (Mathf.Abs(positionOffset.z) > 6.8 && Mathf.Abs(player.GetComponent<Rigidbody>().velocity.z) < 0.3) //if the camera is settled into a position behind the player
             {
                 if (positionOffset.z < 0 && player.GetComponent<Rigidbody>().velocity.normalized.z < 0) //if the camera z is negative flip forwards
                 {
                     currentFlipState = 1;
                 }
                 else //if the camera z is positive flip backwards
                 {
                     currentFlipState = 2;
                 }
             } */
        //}
        //else
        //{
        //float currFOV = camera.fieldOfView;
        //  camera.fieldOfView = Mathf.Lerp(currFOV, defaultFOV, 0.05f);
        //  Vector3 currAngle = transform.rotation.eulerAngles;
        // transform.rotation.eulerAngles.Set(Mathf.Lerp(currAngle.x, defaultXAngle, 0.05f), currAngle.y, currAngle.z);
        //}

        /*handle camera flip for turning around
        if (currentFlipState == (int)flipStates.forward)
        {
            if (positionOffset.z < 6.5 && positionOffset.y >= 1.5)
            {
                positionOffset = Quaternion.AngleAxis(turnSpeed*2, Vector3.right) * positionOffset;
            }
            else
            {
                positionOffset.z = 7;
                positionOffset.y = 2;
                currentFlipState = (int)flipStates.stopped;
            }
            if(player.GetComponent<Rigidbody>().velocity.normalized.z > 0)
            {
                currentFlipState = (int)flipStates.backward;
            }
        }
        if (currentFlipState == (int)flipStates.backward)
        {
            if (positionOffset.z > -6.5 && positionOffset.y >= 1.5)
            {
                positionOffset = Quaternion.AngleAxis(-turnSpeed*2, Vector3.right) * positionOffset;
            }
            else
            {
                positionOffset.z = -7;
                positionOffset.y = 2;
                currentFlipState = (int)flipStates.stopped;
            }
            if (player.GetComponent<Rigidbody>().velocity.normalized.z < 0)
            {
                currentFlipState = (int)flipStates.backward;
            }
        }
        */
        //if (Input.GetKey(KeyCode.A))
        //{
        //      positionOffset = Quaternion.AngleAxis(-turnSpeed, Vector3.up) * positionOffset;
        //        Vector3 currAngle = transform.rotation.eulerAngles;
        //transform.rotation.eulerAngles.Set(currAngle.x, currAngle.y, Mathf.Lerp(currAngle.z, leftZAngle, 0.05f));
        //  xRotationOffset = Mathf.Lerp(xRotationOffset, leftRotationOffset, 0.05f);
        //}
        //if (Input.GetKey(KeyCode.D))
        //{
        //    positionOffset = Quaternion.AngleAxis(turnSpeed, Vector3.up) * positionOffset;
        //            Vector3 currAngle = transform.rotation.eulerAngles;

        //  xRotationOffset = Mathf.Lerp(xRotationOffset, rightRotationOffset, 0.05f);
        //}
        //else
        //{
        //  xRotationOffset = Mathf.Lerp(xRotationOffset, 0, 0.05f);
        //}
        /*
        Vector3 screenPos = GetComponent<Camera>().WorldToScreenPoint(player.transform.position);
        Ray ray = GetComponent<Camera>().ScreenPointToRay(screenPos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)){
            if (hit.transform.tag != "Player")
            {
                positionOffset.z += 0.1f;
//                positionOffset.y += 0.1f;
            }
            else
            {
                if (positionOffset.z > defaultOffset.z)
                {
                    positionOffset.z -= 0.1f;
                }
            }
        }
        */
        //{
        //    var objectHit: Transform = hit.transform;
        //    Debug.Log(objectHit);
        //}

        Vector3 vectorToPlayer = player.transform.position - transform.position;
        Vector3 newOffset = vectorToPlayer.normalized * positionOffset.z;
        newOffset.y = positionOffset.y;


        //lastDirection = player.GetComponent<Rigidbody>().velocity.normalized;
        /*
        if (lastDirection.x < 0)
        {
            positionOffset = Quaternion.AngleAxis(-turnSpeed, Vector3.up) * positionOffset;
        }else if(lastDirection.x > 0)
        {
            positionOffset = Quaternion.AngleAxis(turnSpeed, Vector3.up) * positionOffset;
        }
        */
        transform.position = Vector3.Lerp(transform.position, player.transform.position + newOffset, 10f * Time.deltaTime);
        transform.LookAt(player.transform);
    }
}

