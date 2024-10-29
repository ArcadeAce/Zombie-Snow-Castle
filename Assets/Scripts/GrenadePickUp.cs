using UnityEngine;

/* The `GrenadePickUp` script in your Unity game is designed to handle the behavior when the player picks up a grenade.
This script is attached to a grenade pickup object in the game world.

The script starts by using the `OnTriggerEnter` method, which is called when another collider enters the trigger
collider attached to the grenade pickup object. The method takes a `Collider` parameter named `other`, which represents
the collider that entered the trigger.

Inside the `OnTriggerEnter` method, the script first checks if the collider that entered the trigger belongs to the
player by comparing its tag to "Player".

If the collider belongs to the player, the script attempts to get the `GrenadeThrowHandler` component from the player
object. This component is responsible for managing the player's grenades.

If the `GrenadeThrowHandler` component is found, the script calls the `PickUpGrenade` method on the handler to increase
the player's grenade count.

If the `GrenadeThrowHandler` component is not found on the player, the script logs an error message.

Finally, after the grenade is picked up, the script deactivates the grenade pickup object to prevent it from being
picked up again.

In summary, this script detects when the player collides with a grenade pickup, increases the player's grenade count,
and deactivates the pickup object to ensure it can only be picked up once.

*/

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











