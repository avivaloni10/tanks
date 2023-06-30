using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    public GameObject destroyedMonkey;

    private void OnMouseDown()
    {
        Instantiate(destroyedMonkey, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Shell")){
            Instantiate(destroyedMonkey, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

}
