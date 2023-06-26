using UnityEngine;
using UnityEngine.AI;

public class EnemyTankMovement : MonoBehaviour
{
    public Transform[] waypoints; // Array of waypoints for the enemy tank to patrol
    public float patrolSpeed = 5f; // Speed at which the tank patrols between waypoints
    public float chaseSpeed = 8f; // Speed at which the tank chases the player
    public float chaseDistance = 10f; // Distance at which the tank starts chasing the player

    private NavMeshAgent agent;
    private int currentWaypointIndex;
    private Transform player;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform; // Assumes the player tank has the "Player" tag
        currentWaypointIndex = 0;

        // Start patrolling
        Patrol();
    }

    private void Update()
    {
        // Check if the player is within the chase distance
        if (Vector3.Distance(transform.position, player.position) <= chaseDistance)
        {
            // Chase the player
            Chase();
        }
        else
        {
            // Continue patrolling
            Patrol();
        }
    }

    private void Patrol()
    {
        // Set the agent's destination to the current waypoint
        agent.SetDestination(waypoints[currentWaypointIndex].position);
        agent.speed = patrolSpeed;

        // Check if the agent has reached the current waypoint
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            // Move to the next waypoint
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
    }

    private void Chase()
    {
        // Set the agent's destination to the player's position
        agent.SetDestination(player.position);
        agent.speed = chaseSpeed;
    }
}
