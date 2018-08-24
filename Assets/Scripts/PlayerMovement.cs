using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Collider))]

public class PlayerMovement : MonoBehaviour {

    float accelFactor = 280;
    float lastVelocity;
    //public float turnSpeed = 7f;
    public float velocityMagnitude = 0;
    //ParticleSystem particles;
    Camera camera;
    public GameObject movementObj;
    Text speedometer;
    Vector3 lastDirection;
    Rigidbody playerRigidbody;

    //JOYCON GYRO CONTROLS
    private List<Joycon> joycons;
    public Vector3 accel;
    public int jc_ind = 0;

    // Use this for initialization
    void Start () {
        camera = Camera.main;
        speedometer = GameObject.Find("Speedometer").GetComponent<Text>();
        playerRigidbody = gameObject.GetComponent<Rigidbody>();

        //JOYCONS START
        accel = new Vector3(0, 0, 0);
        // get the public Joycon array attached to the JoyconManager in scene
        if (JoyconManager.Instance != null)
        {
            joycons = JoyconManager.Instance.j;
        }
    }

    // Update is called once per frame
    void Update () {

    }
    private void FixedUpdate()
    {
        // make sure the Joycon only gets checked if attached
        if (joycons != null && joycons.Count > 0)
        {
            Joycon j = joycons[jc_ind];

            // Gyro values: x, y, z axis values (in radians per second)
            accel = j.GetAccel();
            print(accel);
            if (accel.x != 0)
            {
                playerRigidbody.AddForce(movementObj.transform.forward * accelFactor * -accel.x);
                velocityMagnitude = playerRigidbody.velocity.magnitude;
            }
            if(accel.y != 0)
            {
                playerRigidbody.AddForce(movementObj.transform.right * accelFactor * -accel.y);
                velocityMagnitude = playerRigidbody.velocity.magnitude;
            }


        }
        else
        {
            if (Input.GetKey(KeyCode.W))
            {
                //Physics.gravity = Vector3.Slerp(Physics.gravity, maxForwardGravity, 0.5f);
                //playerRigidbody.drag = accelFactor / 50;
                playerRigidbody.AddForce(movementObj.transform.forward * accelFactor);
                velocityMagnitude = playerRigidbody.velocity.magnitude;
            }
            if (Input.GetKey(KeyCode.S))
            {
                //   if (camera.GetComponent<ThirdPersonCamera>().currentFlipState != 0)
                // {
                //   playerRigidbody.AddForce(-camera.transform.forward * accelFactor / 15);
                //  velocityMagnitude = GetComponent<Rigidbody>().velocity.magnitude;
                // }
                // else
                // {
                //    //playerRigidbody.drag = accelFactor / 10;
                //if (GetComponent<Rigidbody>().velocity.magnitude < 3f)
                //{
                playerRigidbody.AddForce(-movementObj.transform.forward * accelFactor);
                velocityMagnitude = playerRigidbody.velocity.magnitude;
                //}
                //Physics.gravity = Vector3.Slerp(Physics.gravity, maxBackwardGravity, 0.5f);
            }

            if (Input.GetKey(KeyCode.A))
            {
                //if (GetComponent<Rigidbody>().velocity.magnitude < 3f)
                //{
                //    Vector3 oldRot = playerRigidbody.rotation.eulerAngles;
                //transform.Rotate(transform.up, -turnSpeed);
                playerRigidbody.AddForce(-movementObj.transform.right * accelFactor);
                //}
                //Physics.gravity = Vector3.Slerp(Physics.gravity, maxLeftGravity, 0.5f);

            }
            if (Input.GetKey(KeyCode.D))
            {
                //if (GetComponent<Rigidbody>().velocity.magnitude < 3f)
                //{
                //   Vector3 oldRot = playerRigidbody.rotation.eulerAngles;
                //transform.Rotate(transform.up, turnSpeed);
                playerRigidbody.AddForce(movementObj.transform.right * accelFactor);
                //}
                //Physics.gravity = Vector3.Slerp(Physics.gravity, maxRightGravity, 0.5f);

            }
        }
       // Physics.gravity = Vector3.Slerp(Physics.gravity, Vector3(, 0.5f);

        float currVelocity = playerRigidbody.velocity.magnitude;

        lastVelocity = currVelocity;
        
        if((lastDirection.z > 0 && playerRigidbody.velocity.normalized.z < 0) || (lastDirection.z < 0 && playerRigidbody.velocity.normalized.z > 0) ||
            (lastDirection.x > 0 && playerRigidbody.velocity.normalized.x < 0) || (lastDirection.x < 0 && playerRigidbody.velocity.normalized.x > 0))
        {
            playerRigidbody.angularVelocity = new Vector3(0, 0, 0);
        }

        speedometer.text = "Speed: " + Mathf.Floor(currVelocity);
        lastDirection = playerRigidbody.velocity.normalized;
        if (lastDirection.z > 0)
        {
            print("forward");
        }
        else if (lastDirection.z < 0)
        {
            print("backward");
        }
        if (lastDirection.x > 0)
        {
            print("right");
        }
        else if (lastDirection.x < 0)
        {
            print("left");
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        print("Time: "+Mathf.FloorToInt(collision.impulse.magnitude));
        if (Mathf.Abs(collision.impulse.magnitude) > 10 && collision.impulse.magnitude != 0)
        {
            //we'll play a sound here
            print(collision.impulse.magnitude);
            // make sure the Joycon only gets checked if attached
            if (joycons != null && joycons.Count > 0)
            {
                Joycon j = joycons[jc_ind];
                j.SetRumble(160, 320, 0.6f, Mathf.CeilToInt(Mathf.Abs(collision.impulse.magnitude)));
            }
        }
    }
}
