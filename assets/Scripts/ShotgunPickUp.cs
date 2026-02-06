using UnityEngine;

public class ShotgunPickUp : MonoBehaviour
{
    public Weapon shotgunPrefab;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && shotgunPrefab)
        {
            // Equip the shotgun into slot 2
            PlayerManager.Instance.WeaponSwitcher.Pickup(shotgunPrefab);

            // Set shotgun ammo instantly (no coroutine, no delay)
            PlayerManager.Instance.shotgunShells = 5;
            PlayerManager.Instance.remainingShotgunShells = 0;

            // Update the UI instantly
            GameManager.UIManager.ReloadGun("Shotgun", 5, 0);

            // Hide the pickup object
            gameObject.SetActive(false);
        }
    }
}





