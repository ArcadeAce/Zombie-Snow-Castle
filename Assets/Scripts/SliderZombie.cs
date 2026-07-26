using UnityEngine;

public class SliderZombie : Enemy
{
    [Header("Slider Zombie Throw Script")]
    public SliderZombieThrow sliderZombieThrow;

    [Header("Pitch Settings")]
    public float weakPitchDistance = 20f;
    public float powerfulPitchDistance = 40f;
    public float attackCooldown = 2f;

    private float attackTimer;

    public override void Update()
    {
        base.Update(); // Keep chasing logic from Enemy.cs

        attackTimer -= Time.deltaTime;

        // If player is in range AND cooldown finished
        if (playerInRange && attackTimer <= 0f)
        {
            DecidePitchType();
        }
    }

    private void DecidePitchType()
    {
        float distance = Vector3.Distance(transform.position, PlayerController.Instance.cam.transform.position);

        // Powerful pitch if far away
        if (distance > weakPitchDistance)
        {
            ThrowPowerfulPitch();
        }
        else
        {
            ThrowWeakPitch();
        }

        attackTimer = attackCooldown;
    }

    private void ThrowWeakPitch()
    {
        Animator.SetTrigger("WeakPitch");
        sliderZombieThrow.useSuperPitch = false;
    }

    private void ThrowPowerfulPitch()
    {
        Animator.SetTrigger("PowerfulPitch");
        sliderZombieThrow.useSuperPitch = true;
    }
}

