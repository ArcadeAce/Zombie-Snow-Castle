using UnityEngine;

public class DeathExplosionSound : MonoBehaviour
{
    private DelayedSound delayedSoundManager;

    void Start()
    {
        delayedSoundManager = FindObjectOfType<DelayedSound>();
    }

    void OnDestroy()
    {
        if (gameObject.CompareTag("ZombieBoss") && delayedSoundManager != null)
        {
            delayedSoundManager.PlaySoundAfterDelay();
        }
    }
}



