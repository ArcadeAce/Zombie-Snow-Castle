using UnityEngine;
using UnityEngine.AI;

public class SamuraiZombieBossAnimationControl : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private bool isAnimationPlaying = false;

    private float animationEndThreshold = 0.9f;

    private void Start()
    {
        navMeshAgent = GetComponentInParent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);

        // Stop movement when ANY Samurai idle animation is playing
        if (state.IsName("Samurai idle") ||
            state.IsName("Samurai idle 0") ||
            state.IsName("Samurai idle 1"))
        {
            navMeshAgent.isStopped = true;
            isAnimationPlaying = true;
        }
        else if (isAnimationPlaying && state.normalizedTime >= animationEndThreshold)
        {
            navMeshAgent.isStopped = false;
            isAnimationPlaying = false;
        }
    }
}

