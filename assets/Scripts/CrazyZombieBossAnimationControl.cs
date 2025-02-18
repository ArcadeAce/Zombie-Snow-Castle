using UnityEngine;
using UnityEngine.AI;

public class CrazyZombieBossAnimationControl : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private bool isAnimationPlaying = false;

    // Define normalized time thresholds for animations that should stop movement
    private float animationStartThreshold = 0.0f;
    private float animationEndThreshold = 1f; // Adjust this value based on your animation's length

    private void Start()
    {
        // Get references to the NavMeshAgent and Animator components
        navMeshAgent = GetComponentInParent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Check if any of the specific animations are currently playing
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Crazy zombie hip hop dancing") ||
            animator.GetCurrentAnimatorStateInfo(0).IsName("Crazy zombie taunt") ||
            animator.GetCurrentAnimatorStateInfo(0).IsName("Crazy zombie hip hop robot dance") ||
            animator.GetCurrentAnimatorStateInfo(0).IsName("Crazy zombie fall flat") ||
            animator.GetCurrentAnimatorStateInfo(0).IsName("Crazy zombie dancing running man") ||
            animator.GetCurrentAnimatorStateInfo(0).IsName("Crazy zombie arm wave") ||
            animator.GetCurrentAnimatorStateInfo(0).IsName("Crazy zombie cross punch") || // Added
            animator.GetCurrentAnimatorStateInfo(0).IsName("Crazy zombie headspin start") || // Added
            animator.GetCurrentAnimatorStateInfo(0).IsName("Crazy zombie headspinning") || // Added
            animator.GetCurrentAnimatorStateInfo(0).IsName("Crazy zombie headspin end")) // Added
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
