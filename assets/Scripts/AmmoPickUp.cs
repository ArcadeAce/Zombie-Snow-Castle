using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickUp : MonoBehaviour
{
    public int ammoToAdd = 30; // Number of bullets to add when picking up one clip
    public string ammoType = "Shotgun"; // Type of ammunition (e.g., "Shotgun")

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter called for " + ammoType + " ammo, Object Name: " + gameObject.name);

        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered trigger for " + ammoType + " ammo");

            // Play the pickup sound
            AudioManager.Instance.PlayEffect("Shotgunammopickup");

            // Update the player's ammunition count based on the ammo type
            PlayerManager.Instance.AddAmmo(ammoType, ammoToAdd);
            Debug.Log("Ammo count updated for " + ammoType);

            // Destroy the ammo pickup object
            Destroy(gameObject, 0.25f);
            Debug.Log($"{ammoType} ammo pickup object destroyed, Object Name: {gameObject.name}");
        }
    }
}













