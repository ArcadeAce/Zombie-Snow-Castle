using UnityEngine;

public class CurveballZombie : Enemy
{
    [Header("Curveball Zombie Throw Script")]
    public CurveballZombieThrow curveballZombieThrow;

    [Header("Pitch Timing")]
    public float pitchInterval = 4f;   // Every 10 seconds
    private float pitchTimer = 0f;

    private bool nextIsSuper = false;   // Controls the cycle

    public override void Update()
    {
        base.Update(); // Keep chasing logic from Enemy.cs

        pitchTimer += Time.deltaTime;

        if (pitchTimer >= pitchInterval)
        {
            PlayNextPitch();
            pitchTimer = 0f;
        }
    }

    private void PlayNextPitch()
    {
        if (nextIsSuper)
        {
            // Powerful curveball pitch
            curveballZombieThrow.useSuperPitch = true;
            Animator.SetTrigger("PowerfulPitch");
            nextIsSuper = false; // Next time will be weak
        }
        else
        {
            // Weak curveball pitch
            curveballZombieThrow.useSuperPitch = false;
            Animator.SetTrigger("WeakPitch");
            nextIsSuper = true; // Next time will be super
        }
    }
}

