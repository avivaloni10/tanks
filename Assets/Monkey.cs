using UnityEngine;

public class Monkey : MonoBehaviour
{
    public GameObject monkeyPiecesPrefab; // Prefab containing the small pieces
    public int numberOfPieces = 64; // Number of pieces to spawn

    private bool isBroken = false; // Flag to prevent multiple breaks

    // Called when the Monkey object is hit by a shell
    public void Break()
    {
        if (isBroken)
            return;

        // Disable the Monkey object
        gameObject.SetActive(false);

        // Spawn the small pieces
        for (int i = 0; i < numberOfPieces; i++)
        {
            GameObject piece = Instantiate(monkeyPiecesPrefab, transform.position, transform.rotation);
            Rigidbody pieceRigidbody = piece.GetComponent<Rigidbody>();

            // Apply an explosion force to each small piece
            pieceRigidbody.AddExplosionForce(500f, transform.position, 10f);
        }

        // Destroy the Monkey object and the script
        Destroy(gameObject);
        Destroy(this);

        isBroken = true;
    }
}
