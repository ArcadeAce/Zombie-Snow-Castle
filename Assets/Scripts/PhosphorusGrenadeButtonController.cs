using UnityEngine;

namespace StarterAssets
{
    public class PhosphorusGrenadeButtonController : MonoBehaviour
    {
        public GameObject phosphorusGrenadeButton;  // Reference to the Phosphorus grenade button
        public PhosphorusGrenadeThrowHandler _PhosphorusGrenadeThrowHandler;  // Updated reference

        private bool isPhosphorusGrenadeThrown = false;

        private void Start()
        {
            if (phosphorusGrenadeButton != null)
            {
                phosphorusGrenadeButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(OnPhosphorusGrenadeButtonPressed);
            }
            else
            {
                Debug.LogError("Phosphorus grenade button reference not assigned!");
            }
        }

        public void OnPhosphorusGrenadeButtonPressed()
        {
            if (isPhosphorusGrenadeThrown)
            {
                return;
            }
            else
            {
                isPhosphorusGrenadeThrown = true;
                StartCoroutine(_PhosphorusGrenadeThrowHandler.PlayTwinTurboDownThenPhosphorusGrenadeThrow());
            }
        }

        public void ResetPhosphorusGrenadeThrow()
        {
            Debug.Log("ResetPhosphorusGrenadeThrow method invoked.");
            isPhosphorusGrenadeThrown = false;
        }
    }
}



