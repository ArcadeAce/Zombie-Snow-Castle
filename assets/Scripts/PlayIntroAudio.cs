using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayIntroAudio : MonoBehaviour
{
    public GameObject punchAudio;
    public void PlayAudio()
    {
        Instantiate(punchAudio);
    }
}
