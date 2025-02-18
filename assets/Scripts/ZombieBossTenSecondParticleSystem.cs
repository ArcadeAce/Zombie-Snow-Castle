using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieBossTenSecondParticleSystem : MonoBehaviour
{
    // Reference to the particle system attached to the zombie.
    public ParticleSystem zombieParticleSystem;

    // The time interval (in seconds) between each particle system activation.
    public float interval = 20f;

    // Timer to keep track of the elapsed time.
    private float timer = 0f;

    private void Start()
    {
        // Check if a ParticleSystem component was assigned.
        if (zombieParticleSystem == null)
        {
            Debug.LogError("No ParticleSystem assigned to the script.");
            enabled = false; // Disable this script to prevent errors.
            return;
        }

        // Initialize the timer to start counting.
        timer = interval;
    }

    private void Update()
    {
        // Decrement the timer by the time elapsed in the last frame.
        timer -= Time.deltaTime;

        // Check if the timer has reached or gone below zero.
        if (timer <= 0f)
        {
            // Trigger the particle system to play.
            PlayParticles();

            // Reset the timer to the specified interval.
            timer = interval;
        }
    }

    // Function to play the particle system.
    private void PlayParticles()
    {
        Debug.Log("Playing particles!"); // Add this line to check if this method is called.

        // Check if the particle system is not already playing.
        if (!zombieParticleSystem.isPlaying)
        {
            // Start playing the particle system.
            zombieParticleSystem.Play();

            // You can also add additional logic here if needed.
            // For example, you can play a sound effect or apply gameplay effects.
        }
    }
}
