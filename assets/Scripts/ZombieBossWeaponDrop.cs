using UnityEngine;

public class ZombieBossWeaponDrop : MonoBehaviour
{
    public GameObject weaponPrefab; // Assign the shotgun prefab in the inspector

    private void OnDestroy()
    {
        DropWeapon();
    }

    private void DropWeapon()
    {
        if (weaponPrefab != null)
        {
            // Instantiate the shotgun prefab at the position of the zombie boss
            GameObject droppedWeapon = Instantiate(weaponPrefab, transform.position, transform.rotation);

            // Ensure the weapon is active
            droppedWeapon.SetActive(true);

            // Ensure the Rigidbody and MeshCollider are set up correctly
            Rigidbody rb = droppedWeapon.GetComponent<Rigidbody>();
            if (rb == null)
            {
                rb = droppedWeapon.AddComponent<Rigidbody>();
            }
            rb.useGravity = true;

            MeshCollider meshCollider = droppedWeapon.GetComponent<MeshCollider>();
            if (meshCollider == null)
            {
                meshCollider = droppedWeapon.AddComponent<MeshCollider>();
            }
            meshCollider.convex = true; // Ensure convex for proper collision

            // Attach the pickup script if not already attached
            if (droppedWeapon.GetComponent<ShotgunPickUp>() == null)
            {
                droppedWeapon.AddComponent<ShotgunPickUp>();
            }
        }
        else
        {
            Debug.LogError("Weapon prefab is not assigned");
        }
    }
}





