using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMovement : MonoBehaviour
{
    
    public Camera camera;

    public float speed = 2.0f;

    private float distanceBtwnObjects = 2;

    private static int randAngle;

    private Vector3 startPos;

    public float distance = 4;

    private Vector3 newPos = new Vector3(10000,10000,10000);

    private Vector3 newDir;

    // Use this for initialization
    void Start()
    {
        randAngle = Random.Range(0, 8);
        switch (randAngle)
        {
            case 0:
                randAngle = 0;
                break;
            case 1:
                randAngle = 45;
                distance = 0.70710678118f * distance;
                break;
            case 2:
                randAngle = 90;
                break;
            case 3:
                randAngle = 135;
                distance = 0.70710678118f * distance;
                break;
            case 4:
                randAngle = 180;
                break;
            case 5:
                randAngle = 225;
                distance = 0.70710678118f * distance;
                break;
            case 6:
                randAngle = 270;
                break;
            case 7:
                randAngle = 315;
                distance = 0.70710678118f * distance;
                break;
            case 8:
                randAngle = 360;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (newPos.x == 10000)
        {
            startPos = transform.position;
            Vector2 direction = (Vector2)(Quaternion.Euler(0, 0, randAngle) * Vector2.right);
            newDir = new Vector3(direction.x, 0, direction.y);
            newDir = newDir.normalized;
            print(newDir);
            newPos = new Vector3(startPos.x + distance * newDir.x, startPos.y, startPos.z + distance * newDir.z);
        }
        transform.position += newDir * speed * Time.deltaTime;
        if(Vector3.Distance(transform.position, newPos) <= 0.05 )
        {
            transform.position = startPos;
        }
        //Vector3 viewPos = camera.WorldToViewportPoint(transform.position);
        //if (viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1 && viewPos.z > 0)
        //{
        //}
        //else
        /*{
            if (randAngle >= 0 && randAngle <= 90)
            {
                randAngle = Random.Range(180, 270);
            }
            else if (randAngle >= 90 && randAngle <= 180)
            {
                randAngle = Random.Range(270, 360);
            }else if (randAngle >= 180 && randAngle <= 270)
            {
                randAngle = Random.Range(0, 90);
            }
            else if (randAngle >= 270 && randAngle <= 360)
            {
                randAngle = Random.Range(90, 180);
            }
        }*/
    }
}