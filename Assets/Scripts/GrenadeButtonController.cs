using System.Collections;
using UnityEngine;

/* The `GrenadeButtonController` script in your Unity project is responsible for handling the behavior when a grenade
button is pressed. It manages the animations and the actual throwing of the grenade.

The script starts by defining several public variables that can be set in the Unity editor.

These variables include references to the grenade button, animators for the arm and weapon, a handler for throwing the
grenade, and the weapon holder object.

In the `Start` method, the script adds a listener to the grenade button's `onClick` event to call the
`OnGrenadeButtonPressed` method when the button is pressed.

The `Awake` method initializes the `weaponholder` by finding the game object named "Weapon Holder".

When the grenade button is pressed, the `OnGrenadeButtonPressed` method checks if a grenade has already been thrown
and if the animator references are not null. If these conditions are met, it sets the `isGrenadeThrown` flag to true
and starts the coroutine `PlayTwinTurboDownThenGrenadeThrow`.

The `PlayTwinTurboDownThenGrenadeThrow` coroutine handles the sequence of animations and the grenade throw. It first
plays the "Twin turbos weapon down" animation, waits for it to complete, then plays the "Hand for grenade" animation,
and finally plays the "Twin turbos weapon up" animation.

After the animations are complete, the `isGrenadeThrown` flag is reset to allow the next grenade throw, and the
animation states are reset.

The `ThrowGrenade` method is called from an animation event. It checks if the `_GrenadeThrowHandler` reference is set
and if the player has grenades left. If so, it calls the `ThrowGrenade` method on the `GrenadeThrowHandler`.

In summary, this script manages the interaction with the grenade button, triggering the appropriate animations and
handling the logic for throwing a grenade in your Unity project.

*/

namespace StarterAssets
{
    public class GrenadeButtonController : MonoBehaviour
    {
        public GameObject grenadeButton;
        public Animator armAnimator;
        public Animator twinTurboAnimator;
        public GrenadeThrowHandler _GrenadeThrowHandler;
        public GameObject weaponholder;
        private bool isGrenadeThrown = false;

        private void Start()
        {
            // Add listener for grenade button
            if (grenadeButton != null)
            {
                grenadeButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(OnGrenadeButtonPressed);
            }
            else
            {
                Debug.LogError("Grenade button reference not assigned!");
            }
        }

        private void Awake()
        {
            weaponholder = GameObject.Find("Weapon Holder");
        }

        public void OnGrenadeButtonPressed()
        {
            if (!isGrenadeThrown && armAnimator != null && twinTurboAnimator != null)
            {
                isGrenadeThrown = true;
                StartCoroutine(PlayTwinTurboDownThenGrenadeThrow());
            }
            else
            {
                Debug.LogError($"Either grenade already thrown or animator references missing! {isGrenadeThrown} {armAnimator}{twinTurboAnimator}");

            }
        }

        private IEnumerator PlayTwinTurboDownThenGrenadeThrow()
        {
            twinTurboAnimator = weaponholder.transform.GetComponentInChildren<Animator>();

            // Play Twin Turbos weapon down animation
            twinTurboAnimator.Play("Twin turbos weapon down");
            Debug.Log("****Twin Turbos weapon down animation triggered.****");
            yield return new WaitUntil(() => twinTurboAnimator.GetCurrentAnimatorStateInfo(0).IsName("Twin turbos weapon down"));
            yield return new WaitForSeconds(twinTurboAnimator.GetCurrentAnimatorStateInfo(0).length);

            // Play Hand for grenade animation
            armAnimator.Play("Hand for grenade");
            Debug.Log("****Hand for grenade animation triggered.****");
            yield return new WaitUntil(() => armAnimator.GetCurrentAnimatorStateInfo(0).IsName("Hand for grenade")); Debug.Log("****finished hand for grenade animation****");
            Debug.Log($"****state name {armAnimator.GetCurrentAnimatorStateInfo(0).shortNameHash} {Animator.StringToHash("Hand for grenade")} {Animator.StringToHash("New State")}");
            Debug.Log("****Grenade time " + armAnimator.GetCurrentAnimatorStateInfo(0).length);
            yield return new WaitForSeconds(armAnimator.GetCurrentAnimatorStateInfo(0).length); Debug.Log("****Twin turbos weapon up should be here****");

            // Play Twin Turbos weapon up animation
            twinTurboAnimator.Play("Twin turbos weapon up");
            Debug.Log("****Twin Turbos weapon up animation triggered.****");
            yield return new WaitUntil(() => twinTurboAnimator.GetCurrentAnimatorStateInfo(0).IsName("Twin turbos weapon up"));
            yield return new WaitForSeconds(twinTurboAnimator.GetCurrentAnimatorStateInfo(0).length);

            // Reset flag to allow next grenade throw
            isGrenadeThrown = false;

            // Reset the animation states to allow the next throw
            armAnimator.Play("New State");
            twinTurboAnimator.Play("New State");
            Debug.Log("****Animation states reset for next grenade throw.");
        }

        public void ThrowGrenade()
        {
            Debug.Log("Called ThrowGrenade from animation event");

            if (_GrenadeThrowHandler != null && PlayerManager.Instance.numberOfGrenades > 0)
            {
                _GrenadeThrowHandler.ThrowGrenade();
                Debug.Log("ThrowGrenade called in GrenadeThrowHandler.");
            }
            else
            {
                Debug.LogError("GrenadeThrowHandler reference not set or no grenades left!");
            }
        }
    }
}





































