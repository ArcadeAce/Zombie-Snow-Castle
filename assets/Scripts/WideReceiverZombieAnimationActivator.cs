using UnityEngine;

public class WideReceiverZombieAnimationActivator : MonoBehaviour
{
    private Animator animator;
    private float footballTimer = 10f; // Time interval for dancing
    public string[] footballAnimations = {
        "Wide receiver zombie quarterback pass", "Wide receiver zombie dancing twerk" // Add the names of your football animations here
    };

    private void Start()
    {
        animator = GetComponent<Animator>(); // Get the Animator component attached to this GameObject
    }

    private void Update()
    {
        footballTimer -= Time.deltaTime;

        if (footballTimer <= 0)
        {
            PlayRandomFootballAnimation();
            footballTimer = 8f; // Reset the timer for the next animation
        }
    }

    private void PlayRandomFootballAnimation()
    {
        if (footballAnimations.Length > 0)
        {
            int randomFootball = Random.Range(0, footballAnimations.Length); // Select a random Football animation index

            animator.SetTrigger("PlayRandomFootballAnimation"); // Set the trigger to transition to the chosen animation
        }
    }
}

