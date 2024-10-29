using System.Collections;
using UnityEngine;

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





































