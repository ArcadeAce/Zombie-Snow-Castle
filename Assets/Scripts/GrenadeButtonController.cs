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

        public GrenadeThrowHandler _GrenadeThrowHandler;

        private bool isGrenadeThrown = false;

        private void Start()
        {
            if (grenadeButton != null)
            {
                grenadeButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(OnGrenadeButtonPressed);
            }
            else
            {
                Debug.LogError("Grenade button reference not assigned!");
            }
        }
        public void OnGrenadeButtonPressed()
        {
            if (isGrenadeThrown)
            {
                return;
            }

            else
            {
                isGrenadeThrown = true;
                StartCoroutine(_GrenadeThrowHandler.PlayTwinTurboDownThenGrenadeThrow());
            }


        }
      
        public void ResetGrenadeThrow()
        {
            Debug.Log("ResetGrenadeThrow method invoked.");
            isGrenadeThrown = false;
        }
    }
}







































