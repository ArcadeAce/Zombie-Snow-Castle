using UnityEngine;

public class TitleScreenMusicPlayer2 : MonoBehaviour
{
    public AudioClip musicClip1;
    public AudioClip musicClip2;
    private AudioSource audioSource1;
    private AudioSource audioSource2;

    void Start()
    {
        audioSource1 = gameObject.AddComponent<AudioSource>();
        audioSource2 = gameObject.AddComponent<AudioSource>();

        PlayRandomMusic();
    }

    void PlayRandomMusic()
    {
        int randomIndex = Random.Range(0, 2);

        if (randomIndex == 0)
        {
            audioSource1.clip = musicClip1;
            audioSource1.Play();
        }
        else
        {
            audioSource2.clip = musicClip2;
            audioSource2.Play();
        }
    }
}



