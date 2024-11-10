using UnityEngine;
using TMPro;
using System.Collections;

/* The `GrenadeThrowHandler` script in your Unity project manages the player's ability to pick up and throw grenades.
It is attached to the player character and interacts with other components to handle grenade-related actions.

The script begins by defining several public variables, including references to the grenade prefab, the throw point,
and UI elements for displaying the grenade count.

In the `Start` method, the script initializes the grenade count UI by calling `UpdateGrenadeUI`.

The `PickUpGrenade` method increases the player's grenade count when a grenade is picked up, provided the player has
not reached the maximum number of grenades. It also updates the UI to reflect the new grenade count.

When the grenade button is pressed, the `OnGrenadeButtonPressed` method checks if the player has grenades available and
if a grenade is not already being thrown. If these conditions are met, it sets a flag to indicate a grenade is being
thrown and schedules the `ThrowGrenade` method to be called after a delay.

The `ThrowGrenade` method handles the actual throwing of the grenade. It instantiates the grenade prefab at the throw
point, applies a force to the grenade to simulate throwing, and decreases the player's grenade count. It also updates
the UI and schedules the `ResetGrenadeThrow` method to reset the throwing flag after a delay.

The `ResetGrenadeThrow` method resets the flag to allow the player to throw another grenade.

Finally, the `UpdateGrenadeUI` method updates the UI elements to display the current grenade count and show or hide the
grenade button based on whether the player has grenades available.

In summary, this script manages the player's grenade inventory, handles the logic for picking up and throwing grenades,
and updates the UI to reflect the current grenade count.
*/

namespace StarterAssets
{
    public class GrenadeThrowHandler : MonoBehaviour
    {
        public GameObject grenadePrefab; // Prefab of the grenade
        public Transform throwPoint; // Transform for the grenade throw position
        public float throwForce = 15f; // Force applied to the thrown grenade
        public float grenadeResetDelay = 0.5f; // Delay before resetting the grenade throw
        public Animator armAnimator;
        public Animator twinTurboAnimator;
        public int maxGrenades = 30; // Maximum number of grenades the player can hold
        public GameObject weaponholder;
        // public int PlayerManager.Instance.numberOfGrenades = 0; // Initialize to 0

        public TextMeshProUGUI grenadeCountText; // Reference to the TextMeshPro for grenade count display
        public GameObject grenadeButton; // Reference to the grenade button GameObject

        private bool isGrenadeThrown = false;

        private void Awake()
        {
            weaponholder = GameObject.Find("Weapon Holder");

            if (armAnimator == null || twinTurboAnimator == null)
            {
                Debug.LogError("One of the animator references is missing!");
                Debug.Log($"arm: {armAnimator}  {twinTurboAnimator}");

            }
        }


        private void Start()
        {
            UpdateGrenadeUI();
        }

        // Method to handle grenade pickup
        public void PickUpGrenade()
        {
            if (PlayerManager.Instance.numberOfGrenades < maxGrenades)
            {
                PlayerManager.Instance.numberOfGrenades++; // Increase grenade count
                Debug.Log("Player picked up a grenade. Total grenades: " + PlayerManager.Instance.numberOfGrenades);
                UpdateGrenadeUI(); // Update UI to reflect new grenade count
            }
        }

        // Method to handle grenade throwing
        public void OnGrenadeButtonPressed()
        {
            Debug.Log("Grenade button pressed.");
            if (PlayerManager.Instance.numberOfGrenades > 0 && !isGrenadeThrown)
            {
                isGrenadeThrown = true;
                StartCoroutine(PlayTwinTurboDownThenGrenadeThrow());
                Invoke(nameof(ThrowGrenade), 1.1f); // Delay to allow for animation timing
                Debug.Log("ThrowGrenade will be invoked after 1.1 seconds.");
            }
            else if (PlayerManager.Instance.numberOfGrenades == 0)
            {
                Debug.LogWarning("No grenades available to throw.");
            }
        }

        // Method to actually throw the grenade
        public void ThrowGrenade()
        {
            Debug.Log("ThrowGrenade method invoked.");

            if (throwPoint == null)
            {
                Debug.LogError("ThrowPoint is not assigned!");
                return;
            }

            // Instantiate the grenade at the throw point's position without a parent
            GameObject grenadeClone = Instantiate(grenadePrefab, throwPoint.position, throwPoint.rotation);

            // Ensure the grenade is not parented to any object (fully independent)
            grenadeClone.transform.parent = null;

            // Apply physics to the grenade
            Rigidbody rb = grenadeClone.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
                rb.useGravity = true;
                rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

                Vector3 throwDirection = throwPoint.forward;
                rb.AddForce(throwDirection * throwForce, ForceMode.VelocityChange);
                Debug.Log("Force applied to grenade: " + throwDirection * throwForce);
            }
            else
            {
                Debug.LogError("Rigidbody not found on grenade clone!");
            }

            // Decrease grenade count after throwing
            PlayerManager.Instance.numberOfGrenades--; // Decrease grenade count by 1
            Debug.Log("Grenade thrown. Grenades left: " + PlayerManager.Instance.numberOfGrenades);

            // Update the UI immediately after throwing
            UpdateGrenadeUI();

            Invoke(nameof(ResetGrenadeThrow), grenadeResetDelay);
            Debug.Log("ResetGrenadeThrow will be invoked after delay.");
        }

        // Reset the ability to throw grenades after a delay
        private void ResetGrenadeThrow()
        {
            Debug.Log("ResetGrenadeThrow method invoked.");
            isGrenadeThrown = false;
        }


        // Update the grenade count UI
        private void UpdateGrenadeUI()
        {
            if (grenadeCountText != null && grenadeButton != null)
            {
                // Update the TextMeshPro UI element with the current grenade count
                grenadeCountText.text = PlayerManager.Instance.numberOfGrenades.ToString();
                Debug.Log("UI Updated. Grenade count displayed: " + PlayerManager.Instance.numberOfGrenades);

                // Show the button and text if there is at least one grenade
                if (PlayerManager.Instance.numberOfGrenades > 0)
                {
                    grenadeButton.SetActive(true); // Keep the button visible
                    grenadeCountText.gameObject.SetActive(true); // Keep the text visible
                    Debug.Log("Grenade button and count text set to active. Grenades left: " + PlayerManager.Instance.numberOfGrenades);
                }
                else
                {
                    // Only hide the grenade button and text when no grenades are left
                    grenadeButton.SetActive(false);
                    grenadeCountText.gameObject.SetActive(false);
                    Debug.Log("No grenades left. UI elements hidden.");
                }
            }
            else
            {
                Debug.LogError("GrenadeCountText or GrenadeButton reference is not assigned!");
            }
        }
        public IEnumerator PlayTwinTurboDownThenGrenadeThrow()
        {
            twinTurboAnimator = weaponholder.transform.GetComponentInChildren<Animator>();
            twinTurboAnimator.Play("Twin turbos weapon down");
            Debug.Log("Twin Turbos weapon down animation triggered.");
            yield return new WaitUntil(() => twinTurboAnimator.GetCurrentAnimatorStateInfo(0).IsName("Twin turbos weapon down"));
            yield return new WaitForSeconds(twinTurboAnimator.GetCurrentAnimatorStateInfo(0).length);

            armAnimator.Play("Hand for grenade");
            Debug.Log("Hand for grenade animation triggered.");
            yield return new WaitUntil(() => armAnimator.GetCurrentAnimatorStateInfo(0).IsName("Hand for grenade"));
            Debug.Log("finished hand for grenade animation");
            yield return new WaitForSeconds(armAnimator.GetCurrentAnimatorStateInfo(0).length);
            Debug.Log("Twin turbos weapon up should be here");

            twinTurboAnimator.Play("Twin turbos weapon up");
            Debug.Log("Twin Turbos weapon up animation triggered.");
            yield return new WaitUntil(() => twinTurboAnimator.GetCurrentAnimatorStateInfo(0).IsName("Twin turbos weapon up"));
            yield return new WaitForSeconds(twinTurboAnimator.GetCurrentAnimatorStateInfo(0).length);

            isGrenadeThrown = false;

            armAnimator.Play("New State");
            twinTurboAnimator.Play("New State");
            Debug.Log("Animation states reset for next grenade throw.");

        }
    }
}





