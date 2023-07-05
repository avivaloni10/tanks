using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemeyShooting : MonoBehaviour
{
    public Transform player;               // Reference to the player's transform
    public Transform fireTransform;        // Transform representing the position and direction to fire shells
    public float fireForce = 10f;          // Force at which the tank shoots shells
    public float shootCooldown = 2f;       // Cooldown time between each shot
    public float missProbability = 0.3f;   // Probability of missing the target
    public GameObject enemyShellPrefab;    // Prefab of the enemy shell to shoot

    private bool canShoot = true;

    private void Update()
    {
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
        // Raycast to check if the enemy tank has a clear line of sight to the player
        RaycastHit hit;
        if (Physics.Raycast(fireTransform.position, fireTransform.forward, out hit))
        {
            if (hit.transform == player)
            {
                // Instantiate an enemy shell and set its position and rotation to the fire transform
                GameObject enemyShellInstance = Instantiate(enemyShellPrefab, fireTransform.position, fireTransform.rotation);

                // Apply force to the enemy shell
                Rigidbody enemyShellRigidbody = enemyShellInstance.GetComponent<Rigidbody>();
                enemyShellRigidbody.velocity = fireTransform.forward * fireForce;
            }
        }
    }
}
