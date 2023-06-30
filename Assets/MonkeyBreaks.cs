using UnityEngine;

public class MonkeyBreaks : MonoBehaviour
{
    public GameObject brokenMonkeyPrefab; // Reference to the broken monkey prefab
    public GameObject explosionPrefab; // Reference to the explosion prefab
    public AudioSource explosionAudio; // Reference to the explosion audio source

    private bool isBroken = false; // Flag to track if the monkey is already broken

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the monkey is already broken
        if (isBroken)
            return;

        // Reference to the MonkeyBreakable component attached to the collided object
        MonkeyBreakable monkeyBreakable = collision.gameObject.GetComponent<MonkeyBreakable>();

        // Check if the collided game object has the MonkeyBreakable component
        if (monkeyBreakable != null)
        {
            // Call the Break() method on the Monkey component to break the monkey
            Break();

            // Set the flag to indicate that the monkey is broken
            isBroken = true;
        }
    }

    public void Break()
    {
        // Instantiate the broken monkey prefab at the same position and rotation as the current monkey
        GameObject brokenMonkeyInstance = Instantiate(brokenMonkeyPrefab, transform.position, transform.rotation);

        // Instantiate the explosion prefab at the same position as the current monkey
        GameObject explosionInstance = Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        // Play the explosion sound effect
        explosionAudio.Play();

        // Destroy the current monkey game object
        Destroy(gameObject);
    }
}
