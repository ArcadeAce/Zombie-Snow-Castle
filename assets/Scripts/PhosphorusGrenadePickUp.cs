using UnityEngine;
using UnityEngine.UI;
using TMPro; // For TextMeshPro

public class PhosphorusGrenadePickUp : MonoBehaviour
{
    public static int phosphorusGrenadeCount = 0; // Static to keep the count consistent across multiple grenades
    public TextMeshProUGUI textPhosphorusGrenadeNumber; // Reference to UI text
    public Button phosphorusGrenadeButton; // Reference to the UI button
   
    // Trigger method for when the player enters the collider of the grenade
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object that collided is the player
        if (other.CompareTag("Player"))
        {
            StarterAssets.PhosphorusGrenadeThrowHandler PhosphorusgrenadeThrowHandler = other.GetComponent<StarterAssets.PhosphorusGrenadeThrowHandler>();
            // Increment the grenade count
            if(PhosphorusgrenadeThrowHandler != null) {PhosphorusgrenadeThrowHandler.PickUpPhosphorusGrenade(); }


            // Deactivate (hide) the grenade object from the scene
            gameObject.SetActive(false);
        }
    }
}


