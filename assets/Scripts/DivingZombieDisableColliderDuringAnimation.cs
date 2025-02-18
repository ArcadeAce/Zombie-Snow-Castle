using UnityEngine;

public class DivingZombieDisableColliderDuringAnimation : MonoBehaviour
{
    private Animator animator;
    private Collider zombieCollider;
    public string[] animationsToDisableCollider; // Array to hold the names of the animations

    private void Start()
    {
        animator = GetComponentInParent<Animator>();
        zombieCollider = GetComponentInParent<Collider>();
    }

    private void Update()
    {
        // Check if the current animation matches any in the animationsToDisableCollider array
        bool disableCollider = false;
        foreach (string animationName in animationsToDisableCollider)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName(animationName))
            {
                disableCollider = true;
                break;
            }
        }

        // Disable or enable the collider based on the current animation
        zombieCollider.enabled = !disableCollider;
    }
}


