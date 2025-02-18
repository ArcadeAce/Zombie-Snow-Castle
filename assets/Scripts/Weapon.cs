using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Weapon : MonoBehaviour
{
    // Indicates if the weapon is currently active, the current weapon like the Twin Turbos
    public bool Active;

    // Flag to indicate if the weapon is obtained from a chest, only the Twin Turbos are found in a wooden chest in the whole game.
    public bool chestweapon;
    // Damage inflicted by the weapon, put the damage amount in the box in the Inpector
    public int damage;

    // Reference to the camera attached to the FPS Controller, for the Raycasting of the gun.
    protected Transform fpsCam;

    // Size of the weapon's magazine or clip
    public int clipSize;

    // Total number of bullets the player has for this weapon
    public int totalBullets;

    // Index to identify the weapon (used for setup and switching) Put in index Twin Turbos 1 and Da Blasta 2
    public int index;

    // Reference to the Animator component attached to the weapon
    [HideInInspector] public Animator _animator;

    // Flag to track if the player is currently firing the weapon
    private bool Firing;

    public string weaponType;

    // Called when the script is enabled (e.g., when the weapon becomes active)
    private void OnEnable()
    {
        // Get the Animator component and the camera from the FPS Controller
        _animator = GetComponent<Animator>();
        fpsCam = SceneController.Instance.PlayerController.cam.transform;

        // Set the weapon as active
        Active = true;

        // Update the UI to display bullets in the clip and remaining bullets
        GameManager.UIManager.ReloadGun(weaponType, PlayerManager.Instance.bulletsInClip, PlayerManager.Instance.remainingBullets);
    }

    // Called when the script starts
    public virtual void Start()
    {
        // Custom setup logic can be added here
    }


    // Setup the weapon, called when obtained from a chest or a weapon pickup.
    public void Setup(bool chest)
    {
        if (chest)
        {
            // Adjust the bullets in the clip and remaining bullets for chest weapons
            PlayerManager.Instance.bulletsInClip = clipSize - 10;
            PlayerManager.Instance.remainingBullets = totalBullets - 20;
        }

        // Update the UI to display bullets in the clip and remaining bullets
        GameManager.UIManager.ReloadGun(weaponType, PlayerManager.Instance.bulletsInClip, PlayerManager.Instance.remainingBullets);
    }

    // Drop the weapon (placeholder, can be implemented if needed)
    internal void Drop()
    {
        // Implement drop logic if necessary
    }

    // Reload the weapon
    public void Reload()
    {
        Debug.Log("reload problem");
        int remainingBullets, bulletsInClip;
        switch (weaponType)
        {
            case "TwinTurbos":
                remainingBullets = PlayerManager.Instance.remainingBullets;
                bulletsInClip = PlayerManager.Instance.bulletsInClip;
                break;

            case "Shotgun":
                remainingBullets = PlayerManager.Instance.remainingShotgunShells;
                bulletsInClip = PlayerManager.Instance.shotgunShells;
                break;
            default:
                Debug.Log("don't have a ammo assigned");
                return;
        }
          
        // Check if reloading is allowed (remaining bullets and not full clip)
        if (remainingBullets <= 0 || bulletsInClip == clipSize)
        {
            Debug.Log($"Reload 2 remaining bullets: {remainingBullets} {bulletsInClip} {clipSize}");

            return;
        }

        // Trigger the "Reload" animation
        _animator.SetTrigger("Reload");

        // Calculate the number of bullets needed to fill the clip
        int reload = clipSize - bulletsInClip;

        // Perform the reload based on available bullets
        if (remainingBullets >= reload)
        {
            PlayerManager.Instance.SetBulletsInClip(weaponType, clipSize);
            PlayerManager.Instance.AddAmmo(weaponType,-reload);
        }
        else
        {
            PlayerManager.Instance.SetBulletsInClip(weaponType, remainingBullets);
            PlayerManager.Instance.AddAmmo(weaponType,-remainingBullets);
        }

    }

    // Update is called once per frame
    private void Update()
    {
        Int32 pointerID;

#if UNITY_EDITOR
        pointerID = -1;
#else
        pointerID = 0;
#endif

        // Check for mouse button down (fire button press)
        if (Input.GetMouseButtonDown(0))
        {
            // Check if the mouse is over a UI element
            if (EventSystem.current.IsPointerOverGameObject(pointerID) && EventSystem.current.currentSelectedGameObject != null)
            {
                // Check if the clicked UI element is the "Fire Button"
                if (EventSystem.current.currentSelectedGameObject.gameObject.name == "Fire Button")
                {
                    // Check if there are bullets in the clip
                    if (PlayerManager.Instance.bulletsInClip <= 0)
                    {
                        return;
                    }

                    // Set the firing flag to true
                    Firing = true;
                }
            }
        }

        // Check for mouse button up (fire button release)
        if (Input.GetMouseButtonUp(0))
        {
            // Set the firing flag to false
            Firing = false;
        }

        // If out of bullets, stop firing
        if (PlayerManager.Instance.bulletsInClip <= 0)
        {
            Debug.Log("outofammo");
            Firing = false;
        }

        // Update the Animator parameter "Firegun" based on the firing flag
        _animator.SetBool("Firegun", Firing);
    }

    // Perform the shooting logic (Raycast to hit enemies)
    public virtual void Shoot()
    {
        // Decrease bullets in the clip
        PlayerManager.Instance.bulletsInClip--;

        // Ensure non-negative number of bullets in the clip
        if (PlayerManager.Instance.bulletsInClip < 0)
        {
            PlayerManager.Instance.bulletsInClip = 0;
        }

        // Update UI to display bullets in the clip
        GameManager.UIManager.UpdateBullets(PlayerManager.Instance.bulletsInClip);

        // Perform a Raycast to detect enemies
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.position, fpsCam.forward, out hit, 1000f))
        {
            // Check if the hit object has an Enemy component
            if (hit.collider && hit.collider.transform.TryGetComponent(out Enemy enemy))
            {
                // Deal damage to the enemy
                enemy.TakeDamage(damage, hit);
            }
            // Check if the hit object's parent has an Enemy component
            else if (hit.collider.transform.parent != null)
            {
                if (hit.collider.transform.parent.TryGetComponent(out Enemy enemy2))
                {
                    // Deal damage to the enemy's parent
                    enemy2.TakeDamage(damage, hit);
                }
            }
        }

    }
}
