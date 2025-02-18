using UnityEngine;

public class WideReceiverZombiesDisableColliderDuringTwoAnimations : MonoBehaviour
{
    private Animator animator;
    private Collider zombieCollider;
    public string animationName1; // Name of the first animation
    public string animationName2; // Name of the second animation

    private void Start()
    {
        animator = GetComponentInParent<Animator>();
        zombieCollider = GetComponentInParent<Collider>();
    }

    private void Update()
    {
        // Check if the current animation matches either of the animation names
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(animationName1) || animator.GetCurrentAnimatorStateInfo(0).IsName(animationName2))
        {
            // Disable the collider during the animation
            zombieCollider.enabled = false;
        }
        else
        {
            // Enable the collider when not in the specified animations
            zombieCollider.enabled = true;
        }
    }
}

