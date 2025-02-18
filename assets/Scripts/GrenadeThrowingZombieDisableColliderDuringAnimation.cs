using UnityEngine;

public class GrenadeThrowingZombieDisableColliderDuringAnimation : MonoBehaviour
{
    private Animator animator;
    private Collider zombieCollider;
    public string tauntAnimationName; // Name of the taunt animation

    private void Start()
    {
        animator = GetComponentInParent<Animator>();
        zombieCollider = GetComponentInParent<Collider>();
    }

    private void Update()
    {
        // Check if the current animation matches the taunt animation name
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(tauntAnimationName))
        {
            // Disable the collider during the taunt animation
            zombieCollider.enabled = false;
        }
        else
        {
            // Enable the collider when not in the taunt animation
            zombieCollider.enabled = true;
        }
    }
}
