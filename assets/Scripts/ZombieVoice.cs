using System.Collections;
using UnityEngine;

public class ZombieVoice : MonoBehaviour
{
    private AudioSource audioSource;
    public float soundInterval = 20f; // Time in seconds between each sound

    void Start()
    {
        // Get the AudioSource component from the GameObject
        audioSource = GetComponent<AudioSource>();

        // Check if the AudioSource exists
        if (audioSource != null)
        {
            // Start the Coroutine to play the sound at intervals
            StartCoroutine(PlaySound());
        }
        else
        {
            Debug.LogWarning("No AudioSource found on the GameObject.");
        }
    }

    IEnumerator PlaySound()
    {
        while (true)
        {
            // Play the sound
            audioSource.Play();

            // Wait for the specified interval before playing again
            yield return new WaitForSeconds(soundInterval);
        }
    }
}

