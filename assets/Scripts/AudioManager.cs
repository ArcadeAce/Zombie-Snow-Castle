using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public SoundEffect[] soundEffects;
    public Music[] musics;
    private Dictionary<string, SoundEffect> effectdictionary;
    private Dictionary<string, Music> musicdictionary;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        GetMusic();
        GetSoundeffects();

    }

    private void GetSoundeffects()
    {
        effectdictionary = new Dictionary<string, SoundEffect>();

        foreach (SoundEffect effect in soundEffects)
        {
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            effect.CreateAudioSource(audioSource);
            Debug.Log($"addingsoundeffect {effect.SoundID} - {audioSource.ToString()}");
            effectdictionary.Add(effect.SoundID, effect);
        }
    }

    private void GetMusic()
    {
        musicdictionary = new Dictionary<string, Music>();

        foreach (Music music in musics)
        {
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            music.CreateAudioSource(audioSource);
            musicdictionary.Add(music.SoundID, music);
        }
    }

    public void PlayEffect(string ID)
    {
        Debug.Log($"playingsound {ID}");
        if (effectdictionary.ContainsKey(ID))

        {
            AudioSource source = effectdictionary[ID].AudioSource;
            Debug.Log($"playingsound {ID} - {source}");
            source.spatialBlend = 0.0f;
            source.Play();
        }
        else { Debug.Log("couldnotfindsound"); }
    }

    public void PlayMusic(string ID)
    {
        AudioSource source = musicdictionary[ID].AudioSource;
        if (source && !source.isPlaying)
        {
            source.spatialBlend = 0.0f;
            source.Play();
        }
    }

    public void StopMusic(string ID)
    {
        AudioSource source = musicdictionary[ID].AudioSource;
        if (source && source.isPlaying)
        {
            source.Stop();
        }
    }


    public void StopAllMusic()
    {
        foreach (var music in musicdictionary)
        {
            StopMusic(music.Key);
        }
    }
}
