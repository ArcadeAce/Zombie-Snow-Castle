using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Audio 
{
    public AudioSource AudioSource
    {
        get; private set;
    }
        public string SoundID
    {
        get; private set;
    }
    public AudioClip audioclip;
    [Range(-3f, 3f)]
    public float pitch =1f;
    public bool threeD;

    public virtual void CreateAudioSource(AudioSource audioSource)
    {
        AudioSource = audioSource;
        SoundID = audioclip.name;
        AudioSource.clip = audioclip;
        AudioSource.playOnAwake = false;
        AudioSource.pitch = pitch;
        AudioSource.spatialBlend = threeD ? 1 : 0;
    }

}

[Serializable]
public class Music : Audio
{
    public override void CreateAudioSource(AudioSource audioSource)
    {
        base.CreateAudioSource(audioSource);
        AudioSource.loop = true;
    }
}

[Serializable]
public class SoundEffect : Audio
{
    public override void CreateAudioSource(AudioSource audioSource)
    {
        base.CreateAudioSource(audioSource);
        
    }
}
