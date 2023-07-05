using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject m_EnemyShellPrefab;
    public Transform m_FireTransform;
    public float m_MaxLaunchForce = 30f;
    public float m_FireCooldown = 2f;
    public float m_RaycastDistance = 10f;
    public LayerMask m_RaycastLayerMask;
    public AudioClip m_FireClip;

    private float m_CurrentLaunchForce;
    private float m_FireCooldownTimer;
    private AudioSource m_AudioSource;

    private void Start()
    {
        m_FireCooldownTimer = m_FireCooldown;
        m_AudioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        m_FireCooldownTimer -= Time.deltaTime;

        if (m_FireCooldownTimer <= 0f)
        {
            Transform playerTransform = GameObject.FindGameObjectWithTag("TrainingTank").transform; // Assumes the player tank has the "TrainingTank" tag
            Vector3 directionToPlayer = playerTransform.position - transform.position;
            float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);

            if (angleToPlayer < 30f) // Adjust the angle threshold as needed
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, directionToPlayer, out hit, m_RaycastDistance, m_RaycastLayerMask))
                {
                    if (hit.collider.CompareTag("TrainingTank"))
                    {
                        Fire();
                        return; // Exit the Update method after firing
                    }
                }
            }
        }
    }

    private void Fire()
    {
        m_FireCooldownTimer = m_FireCooldown;
        GameObject shellInstance = Instantiate(m_EnemyShellPrefab, m_FireTransform.position, m_FireTransform.rotation);
        Rigidbody shellRigidbody = shellInstance.GetComponent<Rigidbody>();
        shellRigidbody.velocity = m_MaxLaunchForce * m_FireTransform.forward;

        if (m_AudioSource && m_FireClip)
        {
            m_AudioSource.PlayOneShot(m_FireClip);
        }
    }
}
