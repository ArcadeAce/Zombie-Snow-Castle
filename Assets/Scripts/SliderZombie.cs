using UnityEngine;

public class SliderZombie : Enemy
{
    [Header("Slider Zombie Throw Script")]
    public SliderZombieThrow sliderZombieThrow;

    [Header("Pitch Settings")]
    public float weakPitchDistance = 20f;
    public float powerfulPitchDistance = 40f;

    [Header("Pitch Cooldown")]
    public float pitchCooldown = 12f;   // Throws every 12 seconds
    private float pitchTimer = 0f;

    public override void Update()
    {
        base.Update(); // Keep chasing logic from Enemy.cs

        pitchTimer += Time.deltaTime;

        // Only throw every 12 seconds AND only if player is in range
        if (playerInRange && pitchTimer >= pitchCooldown)
        {
            DecidePitchType();
            pitchTimer = 0f; // Reset timer
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
    }

    private void ThrowWeakPitch()
    {
        Animator.SetTrigger("WeakPitch"); // Plays "Slider zombie slow pitching"
        sliderZombieThrow.useSuperPitch = false;
    }

    private void ThrowPowerfulPitch()
    {
        Animator.SetTrigger("PowerfulPitch"); // Plays "Slider zombie powerful pitch"
        sliderZombieThrow.useSuperPitch = true;
    }
}

