using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameTrigger : MonoBehaviour
{
    // Reference to the player object
    public GameObject player;

    // Trigger event when the player enters the trigger area
    private void OnTriggerEnter(Collider other)
    {
        // Check if the player has entered the trigger
        if (other.gameObject == player)
        {
            // Disable player movement or any other relevant scripts if necessary

            // Load the credits scene
            SceneManager.LoadScene("CreditsScene", LoadSceneMode.Additive);
        }
    }
}


