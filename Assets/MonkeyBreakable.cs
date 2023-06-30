using UnityEngine;

public class MonkeyBreakable : MonoBehaviour
{
    public GameObject monkeyPiecePrefab; // Prefab containing a single piece

    // Called when the Monkey object is hit by a shell
    public void Break()
    {
        // Disable the Monkey object
        gameObject.SetActive(false);

        // Spawn multiple small pieces
        for (int i = 0; i < 64; i++)
        {
            GameObject piece = Instantiate(monkeyPiecePrefab, transform.position, transform.rotation);
            Rigidbody pieceRigidbody = piece.GetComponent<Rigidbody>();

            // Apply an explosion force to each small piece
            pieceRigidbody.AddExplosionForce(500f, transform.position, 10f);
        }

        // Destroy the Monkey object
        Destroy(gameObject);
    }
}
