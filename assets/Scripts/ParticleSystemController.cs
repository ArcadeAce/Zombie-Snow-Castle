using UnityEngine;

public class ParticleSystemController : MonoBehaviour
{
    private ParticleSystem particleSystem;
    private float playbackTime = 12f;

    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();

        // Save the current time scale and set it to unscaled time
        float originalTimeScale = Time.timeScale;
        Time.timeScale = 1f;

        // Simulate the particle system for the desired playback time
        particleSystem.Simulate(playbackTime);

        // Stop the particle system from updating and rendering particles
        particleSystem.Stop();

        // Restore the original time scale
        Time.timeScale = originalTimeScale;
    }
}






