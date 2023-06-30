using UnityEngine;

public class TrainingShellExplosion : MonoBehaviour
{
    public float explosionForce = 500f; // Force applied to the broken pieces
    public float explosionRadius = 5f; // Radius of the explosion

    private void OnTriggerEnter(Collider other)
    {
        MonkeyExplosion monkeyExplosion = other.GetComponent<MonkeyExplosion>();
        if (monkeyExplosion != null)
        {
            // Trigger the monkey explosion if the collided object has the MonkeyExplosion script
            monkeyExplosion.ExplodeMonkey();
        }
    }
}
