using System.Collections;
using UnityEngine;

public class ZombieBossTenSecondAnimation : MonoBehaviour
{
    public Animator animator; // Reference to the Animator component.
    public AnimationClip animationClip; // Reference to the animation clip.
    private bool playAnimation = false; // Flag to control animation playback.

    private void Start()
    {
        StartCoroutine(PlayAnimationRepeatedly());
    }

    private IEnumerator PlayAnimationRepeatedly()
    {
        // Wait for 10 seconds before starting animation playback.
        yield return new WaitForSeconds(10f);
        playAnimation = true;

        while (true)
        {
            if (playAnimation)
            {
                // Play the animation.
                animator.Play(animationClip.name);
            }

            // Wait for 10 seconds.
            yield return new WaitForSeconds(10f);
        }
    }
}








