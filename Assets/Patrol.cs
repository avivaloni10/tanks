using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : MonoBehaviour
{
    public Transform[] waypoints;       // Array of waypoints for the enemy tank to patrol
    public float patrolSpeed = 5f;      // Speed at which the tank patrols between waypoints
    public float chaseSpeed = 8f;       // Speed at which the tank chases the player
    public float chaseDistance = 10f;   // Distance at which the tank starts chasing the player
    public Rigidbody shellPrefab;       // Prefab of the shell to shoot
    public Transform fireTransform;     // Transform representing the position and direction to fire shells
    public float fireForce = 10f;       // Force at which the tank shoots shells
    public float shootCooldown = 2f;    // Cooldown time between each shot
    public float missProbability = 0.3f; // Probability of missing the target

    private NavMeshAgent agent;
    private int currentWaypointIndex;
    private Transform player;
    private bool canShoot = true;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("TrainingTank").transform; // Assumes the player tank has the "TrainingTank" tag
        currentWaypointIndex = 0;

        // Start patrolling
        TankPatrol();
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
            TankPatrol();
        }

        // Check if shooting cooldown is over
        if (!canShoot)
        {
            // Update the cooldown timer
            shootCooldown -= Time.deltaTime;
            if (shootCooldown <= 0f)
            {
                canShoot = true;
                shootCooldown = 2f; // Reset the cooldown time
            }
        }
    }

    private void TankPatrol()
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

        // Check if the enemy tank is facing the player
        Vector3 directionToPlayer = player.position - transform.position;
        float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);
        if (angleToPlayer < 30f) // Adjust the angle threshold as needed
        {
            // Shoot shells at the player if shooting cooldown is over
            if (canShoot)
            {
                if (Random.value > missProbability)
                {
                    Shoot();
                }
                canShoot = false;
            }
        }
    }

    private void Shoot()
    {
        // Instantiate a shell and set its position and rotation to the fire transform
        Rigidbody shellInstance = Instantiate(shellPrefab, fireTransform.position, fireTransform.rotation);

        // Ignore collisions between the tank and its shells
        Physics.IgnoreCollision(shellInstance.GetComponent<Collider>(), GetComponent<Collider>(), true);

        // Apply force to the shell
        shellInstance.velocity = fireTransform.forward * fireForce;
    }
}