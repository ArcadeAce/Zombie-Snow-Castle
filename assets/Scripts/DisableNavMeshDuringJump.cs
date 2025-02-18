using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DisableNavMeshDuringJump : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;

    private void Start()
    {
        // Get references to the NavMeshAgent and Animator components.
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Function to disable the NavMeshAgent during jump animations.
    public void DisableAgentDuringJump()
    {
        // Disable the NavMeshAgent.
        agent.enabled = false;

        // Set a trigger in the Animator to play the jump animation.
        animator.SetTrigger("Jump"); // Assuming you have a trigger named "Jump" in your Animator.
    }

    // Function to re-enable the NavMeshAgent after jump animations.
    public void EnableAgentAfterJump()
    {
        // Re-enable the NavMeshAgent.
        agent.enabled = true;
    }
}

