using UnityEngine;
using TMPro;
using System.Collections;
namespace StarterAssets
{
    public class GrenadeThrowHandler : MonoBehaviour
    {
        public TextMeshProUGUI grenadeCountText; // Reference to the TextMeshPro for grenade count display
        public GameObject grenadePrefab; // Prefab of the grenade
        public GameObject grenadeButton; // Reference to the grenade button GameObject

        public Transform throwPoint; // Transform for the grenade throw position
        public int maxGrenades = 30; // Maximum number of grenades the player can hold
                                     // public int PlayerManager.Instance.numberOfGrenades = 0; // Initialize to 0
        public float throwForce = 15f; // Force applied to the thrown grenade
        public float grenadeResetDelay = 0.5f; // Delay before resetting the grenade throw
        public Animator armAnimator;
        public Animator twinTurboAnimator;
     
        public GameObject weaponholder;
      

        private bool isGrenadeThrown = false;







        private void Awake()
        {
            // Find and assign the weapon holder object in the scene
            weaponholder = GameObject.Find("Weapon Holder");

            if (armAnimator == null || twinTurboAnimator == null)
            {
                Debug.LogError("One of the animator references is missing!");
                Debug.Log($"arm: {armAnimator}  {twinTurboAnimator}");

            }
        }





        private void Start()
        {   // Update the UI with the current count of grenades
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
            Debug.Log("Twin Turbos weapon down animation triggered.");
            twinTurboAnimator = weaponholder.transform.GetComponentInChildren<Animator>();
            twinTurboAnimator.Play("Twin turbos weapon down");
            yield return new WaitUntil(() => twinTurboAnimator.GetCurrentAnimatorStateInfo(0).IsName("Twin turbos weapon down"));
            yield return new WaitForSeconds(twinTurboAnimator.GetCurrentAnimatorStateInfo(0).length);

            
            Debug.Log("Hand for grenade animation triggered.");
            Debug.Log("finished hand for grenade animation");
            armAnimator.Play("Hand for grenade");
            yield return new WaitUntil(() => armAnimator.GetCurrentAnimatorStateInfo(0).IsName("Hand for grenade"));
            yield return new WaitForSeconds(armAnimator.GetCurrentAnimatorStateInfo(0).length);

            Debug.Log("Twin turbos weapon up should be here");
            Debug.Log("Twin Turbos weapon up animation triggered.");
            twinTurboAnimator.Play("Twin turbos weapon up");
            yield return new WaitUntil(() => twinTurboAnimator.GetCurrentAnimatorStateInfo(0).IsName("Twin turbos weapon up"));
            yield return new WaitForSeconds(twinTurboAnimator.GetCurrentAnimatorStateInfo(0).length);

            isGrenadeThrown = false;

            armAnimator.Play("New State");
            twinTurboAnimator.Play("New State");
            Debug.Log("Animation states reset for next grenade throw.");

        }
    }
}





