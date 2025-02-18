using UnityEngine;

public class TitleScreenPlayPunchSound : MonoBehaviour
{
    public AudioSource audioSource;
    public float delayInSeconds = 3f; // Adjust this value in the Inspector for the desired delay.

    private void Start()
    {
        if (audioSource == null)
        {
            Debug.LogError("AudioSource is not assigned!");
            return;
        }

        // Delay the audio playback based on the specified delayInSeconds.
        Invoke("PlayDelayedAudio", delayInSeconds);
    }

    private void PlayDelayedAudio()
    {
        // Play the audio source after the specified delay.
        audioSource.Play();
    }
}

