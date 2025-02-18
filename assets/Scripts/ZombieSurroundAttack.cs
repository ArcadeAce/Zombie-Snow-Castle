using UnityEngine;
using UnityEngine.Animations;

public class ZombieSurroundAttack : MonoBehaviour
{
    public new ParticleSystem particleSystem;
    public AudioSource audioSource;
    public float particleStartDelay = 2f; // Time in seconds to start the particle system.
    public float audioStartDelay = 2f;    // Time in seconds to start the audio.
    public AnimationClip animationToTrigger; // Drag and drop the animation clip here.

    private bool hasPlayed = false;

    private void Start()
    {
        // Disable the particle system and audio source initially
        if (particleSystem != null)
        {
            particleSystem.Stop();
            particleSystem.gameObject.SetActive(false);
        }

        if (audioSource != null)
        {
            audioSource.Stop();
            audioSource.gameObject.SetActive(false);
        }
    }

    // Call this method when the animation clip specified in the Inspector starts
    public void TriggerAnimation()
    {
        if (!hasPlayed)
        {
            // Play the particle system after the specified delay
            if (particleSystem != null)
            {
                Invoke("PlayParticleSystem", particleStartDelay);
            }

            // Play the audio after the specified delay
            if (audioSource != null)
            {
                Invoke("PlayAudio", audioStartDelay);
            }

            hasPlayed = true; // Mark as played to prevent multiple starts
        }
    }

    private void PlayParticleSystem()
    {
        if (particleSystem != null)
        {
            particleSystem.gameObject.SetActive(true);
            particleSystem.Play();
        }
    }

    private void PlayAudio()
    {
        if (audioSource != null)
        {
            audioSource.gameObject.SetActive(true);
            audioSource.Play();
        }
    }
}

