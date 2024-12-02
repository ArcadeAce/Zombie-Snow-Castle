using UnityEngine;

public class GrenadePickUp : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Get GrenadeThrowHandler from player
            StarterAssets.GrenadeThrowHandler grenadeThrowHandler = other.GetComponent<StarterAssets.GrenadeThrowHandler>();

            if (grenadeThrowHandler != null)
            {
                grenadeThrowHandler.PickUpGrenade(); // Increase grenade count in handler
            }
            else
            {
                Debug.LogError("GrenadeThrowHandler reference not found on player!");
            }

            // Deactivate grenade pickup after being picked up
            gameObject.SetActive(false);
        }
    }
}











