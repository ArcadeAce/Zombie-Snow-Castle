using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationParticlePlayer : MonoBehaviour
{
    public GameObject systemleft;
    public GameObject systemright;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void startparticles()
    {
        systemleft.SetActive(true);
        systemright.SetActive(true);
    }
    public void endparticles()
    {
        systemleft.SetActive(false);
        systemright.SetActive(false);
    }
} 


