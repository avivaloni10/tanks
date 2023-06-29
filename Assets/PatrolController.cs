using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolController : MonoBehaviour
{

    public Transform[] waypoints;  // Array of waypoints for the patrol path
    public float delayBetweenPoints = 1f;  // Delay between each patrol point
    public float moveSpeed = 3f;  // Speed at which the agent moves
    private int currentWaypointIndex = 0;  // Current waypoint index
    private bool isPatrolling = false;  // Flag to check if already patrolling

    IEnumerator Patrol()
    {
        isPatrolling = true;  // Set the flag to indicate patrolling

        while (true)
        {
            // Move towards the current waypoint
            Vector3 targetPosition = waypoints[currentWaypointIndex].position;
            while (transform.position != targetPosition)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

                // Rotate towards the current waypoint
                transform.LookAt(targetPosition);

                yield return null;
            }

            // Wait for the specified delay
            yield return new WaitForSeconds(delayBetweenPoints);

            // Update the waypoint index
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Patrol());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
