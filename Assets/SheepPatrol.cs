using UnityEngine;
using System.Collections;

public class SheepPatrol : MonoBehaviour
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
            Vector3 direction = targetPosition - transform.position;
            direction.Normalize();

            while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
            {
                // Calculate the rotation needed to look in the direction of the next waypoint
                Quaternion targetRotation = Quaternion.LookRotation(-direction, Vector3.up);
                // Rotate towards the target rotation
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 200f * Time.deltaTime);

                // Move towards the current waypoint
                transform.position += direction * moveSpeed * Time.deltaTime;

                yield return null;
            }

            // Wait for the specified delay
            yield return new WaitForSeconds(delayBetweenPoints);

            // Update the waypoint index
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
    }

    void Start()
    {
        StartCoroutine(Patrol());
    }
}
