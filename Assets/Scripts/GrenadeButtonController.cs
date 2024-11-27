using UnityEngine;

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







































