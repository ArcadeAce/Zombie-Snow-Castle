using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class DivingZombieAnimationControl : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private bool isAnimationPlaying = false;

    // Define normalized time thresholds for animations that should stop movement
    private float animationStartThreshold = 0.0f;
    private float animationEndThreshold = 0.9f; // Adjust this value based on your animation's length

    // List of animation names that should stop the NavMeshAgent's movement
    private List<string> stopMovementAnimations = new List<string>
    {
        "Diving zombie left dive",
        "Diving zombie roll left",
        "Diving zombie right dive",
        "Diving zombie right roll",
        "Diving zombie stand to roll",
        "Diving Zombie walk backwards",
        "Diving zombie big jump",
        "Diving zombie left dive 0",
        "Diving zombie roll left 0",
        "Diving zombie time crisis dive",
        "Diving zombie roll left 1"
    };

    private void Start()
    {
        // Get references to the NavMeshAgent and Animator components
        navMeshAgent = GetComponentInParent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Check if any of the specific animations are currently playing
        foreach (var animationName in stopMovementAnimations)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName(animationName))
            {
                // Pause the NavMeshAgent's movement
                navMeshAgent.isStopped = true;
                isAnimationPlaying = true;
                return;
            }
        }

        // Resume the NavMeshAgent's movement if the animation has reached the end threshold
        if (isAnimationPlaying && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= animationEndThreshold)
        {
            navMeshAgent.isStopped = false;
            isAnimationPlaying = false;
        }
    }
}


