using UnityEngine;

public class EnemyShell : MonoBehaviour
{
    public float m_ShellSpeed = 20f;              // Speed at which the shell moves
    public float m_MaxLifeTime = 5f;              // Maximum lifetime of the shell
    public float m_ExplosionRadius = 5f;          // Radius of the explosion
    public float m_ExplosionForce = 1000f;        // Force of the explosion
    public float m_MaxDamage = 100f;              // Maximum damage caused by the shell

    private Rigidbody m_Rigidbody;

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        // Set the lifetime of the shell
        Destroy(gameObject, m_MaxLifeTime);
    }

    private void FixedUpdate()
    {
        // Move the shell forward
        Vector3 movement = transform.forward * m_ShellSpeed * Time.deltaTime;
        m_Rigidbody.MovePosition(m_Rigidbody.position + movement);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the shell collided with a tank
        if (other.CompareTag("TrainingTank"))
        {
            // Get the tank's health component
            TankHealth targetHealth = other.GetComponent<TankHealth>();
            if (targetHealth != null)
            {
                // Calculate the damage based on the distance from the explosion
                float distance = Vector3.Distance(transform.position, other.transform.position);
                float damage = CalculateDamage(distance);

                // Apply damage to the tank
                targetHealth.TakeDamage(damage);
            }

            // Apply explosion force to nearby objects
            Collider[] colliders = Physics.OverlapSphere(transform.position, m_ExplosionRadius);
            for (int i = 0; i < colliders.Length; i++)
            {
                Rigidbody targetRigidbody = colliders[i].GetComponent<Rigidbody>();
                if (targetRigidbody != null)
                {
                    targetRigidbody.AddExplosionForce(m_ExplosionForce, transform.position, m_ExplosionRadius);
                }
            }

            // Destroy the shell
            Destroy(gameObject);
        }
    }

    private float CalculateDamage(float distance)
    {
        // Calculate the damage based on the distance from the explosion
        float relativeDistance = (m_ExplosionRadius - distance) / m_ExplosionRadius;
        float damage = relativeDistance * m_MaxDamage;
        damage = Mathf.Max(0f, damage);
        return damage;
    }
}
