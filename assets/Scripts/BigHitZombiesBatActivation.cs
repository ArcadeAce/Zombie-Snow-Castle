using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigHitZombiesBatActivation : MonoBehaviour
{
    public GameObject batOnBack; // Reference to the bat on the zombie's back
    public GameObject batInHand; // Reference to the bat in the zombie's hand

    private Animator animator;

    private string[] attackingAnimationNames = {
        "Big hit zombies standing melee combo attack 3",
        "Big hit zombies standing melee combo attack 2",
        "Big hit zombies standing melee attack 360 low",
        "Big hit zombies standing melee attack 360 high",
        "Big hit zombies combo attack with bat glow"
    };

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        bool isAttacking = false;
        foreach (string animationName in attackingAnimationNames)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName(animationName))
            {
                isAttacking = true;
                break;
            }
        }

        if (isAttacking)
        {
            // Disable the bat on the back and enable the bat in the hand
            ToggleBatVisibility(true);
        }
        else
        {
            // If not in a bat attack animation, re-enable the bat on the back
            ToggleBatVisibility(false);
        }
    }

    private void ToggleBatVisibility(bool inHand)
    {
        batOnBack.SetActive(!inHand);
        batInHand.SetActive(inHand);
    }
}


