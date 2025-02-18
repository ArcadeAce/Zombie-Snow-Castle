using UnityEngine;

public class BaseballMovement : MonoBehaviour
{
    public float speed = 10f;  // Speed of the baseball

    private Transform player;  // Reference to the player's transform

    void Start()
    {
        // Find the player using their tag
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Check if the player is found
        if (player == null)
        {
            Debug.LogError("Player not found! Make sure the player has the 'Player' tag.");
        }
    }

    void Update()
    {
        // Check if the player reference is valid
        if (player != null)
        {
            // Calculate the direction from the baseball to the player
            Vector3 direction = player.position - transform.position;

            // Normalize the direction to get a unit vector
            direction.Normalize();

            // Move the baseball towards the player
            transform.Translate(direction * speed * Time.deltaTime);
        }
    }
}

