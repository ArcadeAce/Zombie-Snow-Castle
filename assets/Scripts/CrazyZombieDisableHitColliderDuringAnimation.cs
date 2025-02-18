using UnityEngine;

public class CrazyZombieDisableHitColliderDuringAnimation : MonoBehaviour
{
    private Animator animator;
    private Collider zombieCollider;

    public string[] danceAnimationNames = {
        "Crazy zombie hip hop dancing",
        "Crazy zombie taunt",
        "Crazy zombie hip hop robot dance",
        "Crazy zombie fall flat",
        "Crazy zombie dancing running man",
        "Crazy zombie arm wave"
    };

    private void Start()
    {
        animator = GetComponentInParent<Animator>();
        zombieCollider = GetComponentInParent<Collider>();
    }

    private void Update()
    {
        bool disableCollider = false;

        // Check if the current animation matches any of the dance animation names
        foreach (string animationName in danceAnimationNames)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName(animationName))
            {
                disableCollider = true;
                break; // No need to check other animations, we found a match
            }
        }

        // Enable or disable the collider based on the animation
        zombieCollider.enabled = !disableCollider;
    }
}

