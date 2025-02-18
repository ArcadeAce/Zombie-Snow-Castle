using UnityEngine;

public class DelayedSound : MonoBehaviour
{
    public AudioClip soundClip;
    public float delay = 1f;

    public void PlaySoundAfterDelay()
    {
        Invoke("PlaySound", delay);
    }

    void PlaySound()
    {
        // Play sound at full volume
        AudioSource.PlayClipAtPoint(soundClip, transform.position, 1f);
    }
}


