using UnityEngine;

public class ShotgunPickUp : MonoBehaviour
{
    public Weapon shotgunPrefab; // Reference to the shotgun prefab.

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player enters the trigger zone.
        if (other.CompareTag("Player"))
        {
            // Pick up the shotgun and equip it.
            if (shotgunPrefab)
            {
                // Use the reference to the shotgun prefab.
                PlayerManager.Instance.WeaponSwitcher.Pickup(shotgunPrefab);

                // Disable this shotgun object.
                gameObject.SetActive(false);
            }
            else
            {
                Debug.LogError("Shotgun prefab is not assigned to the ShotgunPickUp script.");
            }
        }
    }
}


