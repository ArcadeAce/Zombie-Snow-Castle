using UnityEngine;
using UnityEngine.AI;

public class BrokenLegZombieAnimationControl : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private Animator animator;

    private void Start()
    {
        // Get references to the NavMeshAgent and Animator components
        navMeshAgent = GetComponentInParent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Check if the zombie is currently punching
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Broken leg zombie boss punch"))
        {
            // If the punch animation is playing, stop the NavMeshAgent's movement
            navMeshAgent.isStopped = true;
        }
        else
        {
            // If the punch animation is not playing, allow the NavMeshAgent to move
            navMeshAgent.isStopped = false;
        }
    }
}







