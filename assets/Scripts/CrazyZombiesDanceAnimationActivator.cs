using UnityEngine;

public class CrazyZombiesDanceAnimationActivator : MonoBehaviour
{
    private Animator animator;
    private float danceTimer = 5f; // Time interval for dancing
    public string[] danceAnimations = {
        "Crazy zombie hip hop dancing", "Crazy zombie taunt", "Crazy zombie hip hop robot dance", "Crazy zombie fall flat", "Crazy zombie dancing running man", "Crazy zombie arm wave", "Crazy zombie headspin start", "Crazy zombie head spinning", "Crazy zombie headspin end" // Add the names of your dance animations here
    };

    private void Start()
    {
        animator = GetComponent<Animator>(); // Get the Animator component attached to this GameObject
    }

    private void Update()
    {
        danceTimer -= Time.deltaTime;

        if (danceTimer <= 0)
        {
            PlayRandomDanceAnimation();
            danceTimer = 6f; // Reset the timer for the next dance
        }
    }

    private void PlayRandomDanceAnimation()
    {
        if (danceAnimations.Length > 0)
        {
            int randomDance = Random.Range(0, danceAnimations.Length); // Select a random dance animation name
            Debug.Log("Triggering " + danceAnimations[randomDance] + " animation");
            animator.SetTrigger(danceAnimations[randomDance]); // Set a trigger with the animation name
        }
    }
}

