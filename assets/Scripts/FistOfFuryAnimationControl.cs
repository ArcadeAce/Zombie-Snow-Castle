using UnityEngine;
using UnityEngine.AI;

public class FistOfFuryAnimationControl : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private bool isAnimationPlaying = false;

    // Define normalized time thresholds for animations that should stop movement
    private float animationStartThreshold = 0.0f;
    private float animationEndThreshold = 0.9f; // Adjust this value based on your animation's length

    private void Start()
    {
        // Get references to the NavMeshAgent and Animator components
        navMeshAgent = GetComponentInParent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Check if any of the specific animations are currently playing
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Fist of fury quad punch") ||
            animator.GetCurrentAnimatorStateInfo(0).IsName("Fist of fury punching bag") ||
            animator.GetCurrentAnimatorStateInfo(0).IsName("Fist of fury punch to elbow combo") ||
            animator.GetCurrentAnimatorStateInfo(0).IsName("Fist of fury combo punch") ||
            animator.GetCurrentAnimatorStateInfo(0).IsName("Fist of fury fight a") ||
            animator.GetCurrentAnimatorStateInfo(0).IsName("Fist of fury fight b") || // Added
            animator.GetCurrentAnimatorStateInfo(0).IsName("")) // Added
        {
            // Pause the NavMeshAgent's movement
            navMeshAgent.isStopped = true;
            isAnimationPlaying = true;
        }
        else if (isAnimationPlaying && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= animationEndThreshold)
        {
            // Resume the NavMeshAgent's movement if the animation has reached the end threshold
            navMeshAgent.isStopped = false;
            isAnimationPlaying = false;
        }
    }
}

