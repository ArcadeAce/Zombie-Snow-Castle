using UnityEngine;
using UnityEngine.AI;

public class BigHitZombieAnimationControl : MonoBehaviour
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
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Big hit zombies standing melee combo attack 3") ||
            animator.GetCurrentAnimatorStateInfo(0).IsName("Big hit zombies standing melee combo attack 2") ||
            animator.GetCurrentAnimatorStateInfo(0).IsName("Big hit zombies standing melee attack 360 low") ||
            animator.GetCurrentAnimatorStateInfo(0).IsName("Big hit zombies standing melee attack 360 high") ||
            animator.GetCurrentAnimatorStateInfo(0).IsName("Big hit zombies combo attack with bat glow"))
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


