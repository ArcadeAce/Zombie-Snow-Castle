using System;
using UnityEngine;

public class ShootSound : MonoBehaviour
{
    public AudioSource[] Sounds;

    public void PlaySound()
    {
        Sounds[0].Play();
    }

    public void PlayReloadSound()
    {
        Sounds[1].Play();
    }

    public void PlaySpinSound()
    {
        Sounds[2].Play();
    }

    internal void PlayShootSound()
    {
        throw new NotImplementedException();
    }
}