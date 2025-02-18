using UnityEngine;

public class TitleScreenMusicPlayer : MonoBehaviour
{
    public AudioClip musicClip;
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        Invoke("PlayMusic", 3f);
    }

    void PlayMusic()
    {
        audioSource.clip = musicClip;
        audioSource.Play();
    }
}

