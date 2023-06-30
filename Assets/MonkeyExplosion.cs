using UnityEngine;

public class MonkeyExplosion : MonoBehaviour
{
    public GameObject monkeyBreakablePrefab; // Reference to the MonkeyBreakable prefab
    public float explosionForce = 500f; // Force applied to the broken pieces
    public float explosionRadius = 5f; // Radius of the explosion

    public void ExplodeMonkey()
    {
        // Disable the Monkey object
        gameObject.SetActive(false);

        // Enable the MonkeyBreakable prefab at the same position and rotation as the Monkey
        GameObject monkeyBreakable = Instantiate(monkeyBreakablePrefab, transform.position, transform.rotation);

        // Get all the renderers from the broken pieces
        Renderer[] renderers = monkeyBreakable.GetComponentsInChildren<Renderer>();

        // Enable the renderers for the broken pieces
        foreach (Renderer renderer in renderers)
        {
            renderer.enabled = true;
        }

        // Get all the colliders from the broken pieces
        Collider[] colliders = monkeyBreakable.GetComponentsInChildren<Collider>();

        // Enable the colliders for the broken pieces
        foreach (Collider collider in colliders)
        {
            collider.enabled = true;
        }

        // Get all the rigidbodies from the broken pieces
        Rigidbody[] rigidbodies = monkeyBreakable.GetComponentsInChildren<Rigidbody>();

        // Enable the rigidbodies for the broken pieces and apply explosion force
        foreach (Rigidbody rb in rigidbodies)
        {
            rb.isKinematic = false;
            rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
        }

        // Do any additional logic for breaking the Monkey here

        // Destroy the MonkeyBreakable after a certain duration or when appropriate
        Destroy(monkeyBreakable, 5f);
    }
}
