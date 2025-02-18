using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatAnimationController : MonoBehaviour
{
    public GameObject batOnBack; // Reference to the bat on the zombie's back
    public GameObject batInHand; // Reference to the bat in the zombie's hand
    public AnimationClip[] swingingAnimations; // List of swinging animations

    private Animator animator;
    private bool isSwinging;

    private void Start()
    {
        animator = GetComponent<Animator>();
        isSwinging = false;
    }

    private void Update()
    {
        // Check if the current animation is a swinging animation
        foreach (var animClip in swingingAnimations)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName(animClip.name))
            {
                if (!isSwinging)
                {
                    // Disable the bat on the back and enable the bat in the hand
                    ToggleBatVisibility(true);
                    isSwinging = true;
                }
                return;
            }
        }

        // If not in a swinging animation, re-enable the bat on the back
        if (isSwinging)
        {
            ToggleBatVisibility(false);
            isSwinging = false;
        }
    }

    private void ToggleBatVisibility(bool inHand)
    {
        batOnBack.SetActive(!inHand);
        batInHand.SetActive(inHand);
    }
}

