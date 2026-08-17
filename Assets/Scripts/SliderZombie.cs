using UnityEngine;

public class SliderZombie : Enemy
{
    [Header("Slider Zombie Throw Script")]
    public SliderZombieThrow sliderZombieThrow;

    [Header("Pitch Timing")]
    public float pitchInterval = 10f;   // Every 10 seconds
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
            // Powerful pitch
            sliderZombieThrow.useSuperPitch = true;
            Animator.SetTrigger("PowerfulPitch");
            nextIsSuper = false; // Next time will be weak
        }
        else
        {
            // Weak pitch
            sliderZombieThrow.useSuperPitch = false;
            Animator.SetTrigger("WeakPitch");
            nextIsSuper = true; // Next time will be super
        }
    }
}




