using UnityEngine;
using TMPro;
using System.Collections;
using System;

namespace StarterAssets
{
    public class PhosphorusGrenadeThrowHandler : MonoBehaviour
    {
        public GameObject phosphorusGrenadePrefab; // Prefab of the phosphorus grenade
        public Transform phosphorusGrenadeThrowPoint; // Transform for the phosphorus grenade throw position
        public float throwForce = 15f; // Force applied to the thrown grenade
        public float grenadeResetDelay = 0.5f; // Delay before resetting the grenade throw
        public Animator phosphorusArmAnimator; // Animator for the arm holding the phosphorus grenade
        public Animator twinTurboAnimator; // Animator for the twin turbo weapon
        public int maxPhosphorusGrenades = 30; // Maximum number of grenades the player can hold
        public GameObject weaponholder; // Reference to the weapon holder object
        public TextMeshProUGUI phosphorusGrenadeCountText; //Reference to the TextMeshPro for Phosphorus grenade count display
        public GameObject phosphorusGrenadeButton; // Reference to the Phosphorus grenade button GameObject

        /*internal string PlayTwinTurboDownThenPhosphorusGrenadeThrow()
        {
            throw new NotImplementedException();
        }*/
        //UN COMMENT LATER//

        private bool isPhosphorusGrenadeThrown = false;// Boolean flag to indicate if a phosphorus grenade is currently thrown

        private void Awake()
        {
            // Find and assign the weapon holder object in the scene
            weaponholder = GameObject.Find("Weapon Holder");

            // Check if the animator references are assigned and log an error if not
            if (phosphorusArmAnimator == null || twinTurboAnimator == null)
            {
                Debug.LogError("One of the animator references is missing!");
            }
        }







        private void Start()
        {
            // Update the UI with the current count of phosphorus grenades
            UpdatePhosphorusGrenadeUI();
        }

        // Method to handle phosphorus grenade pick up
        public void PickUpPhosphorusGrenade()
        {

            if (PlayerManager.Instance.numberOfPhosphorusGrenades < maxPhosphorusGrenades)
            {

                PlayerManager.Instance.numberOfPhosphorusGrenades++; // Increase count of phosphorus grenades
                // Update the UI with the new count
                UpdatePhosphorusGrenadeUI();
            }
        }

        // Method to handle the event when the phosphorus grenade button is pressed
        public void OnPhosphorusGrenadeButtonPressed()
        {
            Debug.Log("Phosphorus Grenade button pressed.");
            // Check if there are any phosphorus grenades available and if one is not already thrown
            if (PlayerManager.Instance.numberOfPhosphorusGrenades > 0 && !isPhosphorusGrenadeThrown)
            {
                // Set the flag to indicate that a phosphorus grenade is thrown
                isPhosphorusGrenadeThrown = true;

                // Play the animations and throw the grenade after a delay
                StartCoroutine(PlayTwinTurboDownThenPhosphorusGrenadeThrow());
                Invoke(nameof(ThrowPhosphorusGrenade), 1.1f); // Delay to allow for animation timing

            }
        }







        // Method to actually throw the phosphorus grenade
        public void ThrowPhosphorusGrenade()
        {
            // Check if the throw point is assigned
            if (phosphorusGrenadeThrowPoint == null)
            {
                Debug.LogError("Phosphorus Grenade Throw Point is not assigned!");
                return;
            }

            Debug.Log("Throwing Phosphorus Grenade");

            // Instantiate the grenade prefab at the throw point
            GameObject grenadeClone = Instantiate(phosphorusGrenadePrefab, phosphorusGrenadeThrowPoint.position, phosphorusGrenadeThrowPoint.rotation);
            // Detach the grenade from the parent object
            grenadeClone.transform.parent = null;

            // Get the Rigidbody component of the grenade
            Rigidbody rb = grenadeClone.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Set the Rigidbody properties
                rb.isKinematic = false;
                rb.useGravity = true;
                rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
                rb.AddForce(phosphorusGrenadeThrowPoint.forward * throwForce, ForceMode.VelocityChange); // Apply force to the grenade to throw it
            }

            // Decrease grenade count by 1
            PlayerManager.Instance.numberOfPhosphorusGrenades--;

            UpdatePhosphorusGrenadeUI(); // Update the phosphorus grenade UI immediately after throwing

            Invoke(nameof(ResetPhosphorusGrenadeThrow), grenadeResetDelay); // Reset the throw state after a delay
        }

        // Reset the ability to throw phosphorus grenades after delay
        private void ResetPhosphorusGrenadeThrow()
        {
            // Reset the flag to indicate that a phosphorus grenade is no longer thrown
            isPhosphorusGrenadeThrown = false;
            Debug.Log("Phosphorus Grenade Throw Reset");
        }

        // Update the phosphorus grenade count UI
        private void UpdatePhosphorusGrenadeUI()
        {
            if (phosphorusGrenadeCountText != null && phosphorusGrenadeButton != null)
            {
                // Update the TextMeshPro UI element with the current phosphorus grenade count
                phosphorusGrenadeCountText.text = PlayerManager.Instance.numberOfPhosphorusGrenades.ToString();
                // Set the visibility of the button and text based on the count
                phosphorusGrenadeButton.SetActive(PlayerManager.Instance.numberOfPhosphorusGrenades > 0);
                phosphorusGrenadeCountText.gameObject.SetActive(PlayerManager.Instance.numberOfPhosphorusGrenades > 0);
            }
        }






        // Coroutine to handle the animations before and after throwing the grenade
        public IEnumerator PlayTwinTurboDownThenPhosphorusGrenadeThrow()
        {

            // Play the "Twin turbos weapon down" animation
            twinTurboAnimator.Play("Twin turbos weapon down");
            Debug.Log("Playing Twin Turbo Down Animation");
            yield return new WaitUntil(() => twinTurboAnimator.GetCurrentAnimatorStateInfo(0).IsName("Twin turbos weapon down"));
            yield return new WaitForSeconds(twinTurboAnimator.GetCurrentAnimatorStateInfo(0).length);


            // Play the "Hand for phosphorus grenade" animation
            phosphorusArmAnimator.Play("Hand for phosphorus grenade");
            Debug.Log("Playing Hand for Phosphorus Grenade Animation");
            yield return new WaitUntil(() => phosphorusArmAnimator.GetCurrentAnimatorStateInfo(0).IsName("Hand for phosphorus grenade"));
            yield return new WaitForSeconds(phosphorusArmAnimator.GetCurrentAnimatorStateInfo(0).length);

            Debug.Log("Playing Twin Turbo Up Animation");
            // Play the "Twin turbos weapon up" animation
            twinTurboAnimator.Play("Twin turbos weapon up");
            yield return new WaitUntil(() => twinTurboAnimator.GetCurrentAnimatorStateInfo(0).IsName("Twin turbos weapon up"));
            yield return new WaitForSeconds(twinTurboAnimator.GetCurrentAnimatorStateInfo(0).length);

            // Reset the flag to indicate that a phosphorus grenade is no longer thrown
            isPhosphorusGrenadeThrown = false;

            // Reset the animators to their default states
            phosphorusArmAnimator.Play("New State");
            twinTurboAnimator.Play("New State");
        }
    }
}

