using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class Destructible : MonoBehaviour
{
    public GameObject destroyedPrefab;

    private void OnMouseDown()
    {
        Instantiate(destroyedPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Shell")){
            Instantiate(destroyedPrefab, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
