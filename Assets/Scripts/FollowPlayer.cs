// Patrol.cs
using UnityEngine;
using UnityEngine.AI;
using System.Collections;


public class FollowPlayer : MonoBehaviour
{

    public Transform[] points;
    private int destPoint = 0;
    private NavMeshAgent agent;
    private float startingHeight;
    private Vector3 newPos;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //startingHeight = transform.position.y;

        // Disabling auto-braking allows for continuous movement
        // between points (ie, the agent doesn't slow down as it
        // approaches a destination point).
        agent.autoBraking = false;
        if (points.Length == 0)
            return;
        newPos = new Vector3(points[destPoint].position.x, transform.position.y, points[destPoint].position.z);
        agent.destination = newPos;
    }


    void Update()
    {
        if (points.Length == 0)
            return;
        newPos = new Vector3(points[destPoint].position.x, transform.position.y, points[destPoint].position.z);
        agent.destination = newPos;
    }
}