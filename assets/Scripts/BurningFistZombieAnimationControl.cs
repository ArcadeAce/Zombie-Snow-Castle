using UnityEngine;
using UnityEngine.AI;

public class BurningFistZombieAnimationControl : MonoBehaviour
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
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Burning fist zombie cross punch") ||
            animator.GetCurrentAnimatorStateInfo(0).IsName("Burning fist zombie quad punch side") ||
            animator.GetCurrentAnimatorStateInfo(0).IsName("Burning fist zombie punching") ||
            animator.GetCurrentAnimatorStateInfo(0).IsName("Burning fist zombie quad punch low") ||
            animator.GetCurrentAnimatorStateInfo(0).IsName("Burning fist zombie quad punch high") ||
            animator.GetCurrentAnimatorStateInfo(0).IsName("Burning fist zombie combo dodging"))
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




