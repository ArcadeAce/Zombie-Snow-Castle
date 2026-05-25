using System;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.EventSystems;
using Debug = UnityEngine.Debug;


public class Weapon : MonoBehaviour
{
    public int damage;                     // How much damage this weapon deals
    public int clipSize;                   // Max bullets in the clip
    public int totalBullets;               // Total bullets player has for this weapon
    protected Transform fpsCam;            // Camera reference for raycast shooting

    public int index;                      // Weapon index for switching
    public bool Active;                    // Is this weapon currently active?
    public bool chestweapon;               // For future chest pickup logic

    [HideInInspector] public Animator _animator; // Animator for firing/reloading animations

    private bool Firing;                   // Tracks if the weapon is currently firing

    public string weaponType;              // "TwinTurbos" or "Shotgun"
    internal int weaponIndex;              // Internal index for WeaponSwitcher


    // ===========================
    //  BASE SHOOT() — Twin Turbos
    // ===========================
    public virtual void Shoot()
    {
        // Raycast forward from the FPS camera
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.position, fpsCam.forward, out hit, 1000f))
        {
            // Direct hit on enemy
            if (hit.collider && hit.collider.transform.TryGetComponent(out Enemy enemy))
            {
                enemy.TakeDamage(damage, hit);
            }
            // Hit a child object of an enemy
            else if (hit.collider.transform.parent != null)
            {
                if (hit.collider.transform.parent.TryGetComponent(out Enemy enemy2))
                {
                    enemy2.TakeDamage(damage, hit);
                }
            }
        }
    }


    // ===========================
    //  ONENABLE — When weapon becomes active
    // ===========================
    private void OnEnable()
    {
        _animator = GetComponent<Animator>();
        fpsCam = PlayerController.Instance.cam.transform;

        Active = true;

        // Update UI based on weapon type
        if (weaponType == "Shotgun")
        {
            int current = PlayerManager.Instance.shotgunShells;
            int total = PlayerManager.Instance.remainingShotgunShells;

            GameManager.UIManager.ReloadGun("Shotgun", current, total);
        }
        else if (weaponType == "TwinTurbos")
        {
            int current = PlayerManager.Instance.bulletsInClip;
            int total = PlayerManager.Instance.remainingBullets;

            GameManager.UIManager.ReloadGun("TwinTurbos", current, total);
        }
    }


    // ===========================
    //  START — Empty on purpose
    // ===========================
    public virtual void Start()
    {
        // Child classes override if needed
    }


    // ===========================
    //  SETUP — For chest pickups
    // ===========================
    public void Setup(bool chest)
    {
        if (chest)
        {
            // Optional chest logic
        }

        GameManager.UIManager.ReloadGun(
            weaponType,
            PlayerManager.Instance.bulletsInClip,
            PlayerManager.Instance.remainingBullets
        );
    }


    // ===========================
    //  DROP — Placeholder
    // ===========================
    internal void Drop()
    {
        // Future weapon drop logic
    }


    // ===========================
    //  RELOAD — Works for both weapons
    // ===========================
    public virtual void Reload()
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

        if (remainingBullets <= 0 || bulletsInClip == clipSize)
        {
            Debug.Log($"Reload 2 remaining bullets: {remainingBullets} {bulletsInClip} {clipSize}");
            return;
        }

        _animator.SetTrigger("Reload");

        int reload = clipSize - bulletsInClip;

        if (remainingBullets >= reload)
        {
            PlayerManager.Instance.SetBulletsInClip(weaponType, clipSize);
            PlayerManager.Instance.AddAmmo(weaponType, -reload);
        }
        else
        {
            PlayerManager.Instance.SetBulletsInClip(weaponType, remainingBullets);
            PlayerManager.Instance.AddAmmo(weaponType, -remainingBullets);
        }
    }


    // ===========================
    //  UPDATE — FIXED FOR SHOTGUN
    // ===========================
    private void Update()
    {
        Int32 pointerID;

#if UNITY_EDITOR
        pointerID = -1;
#else
        pointerID = 0;
#endif

        // 🔫 Determine correct ammo based on weapon type
        int currentAmmo = weaponType == "Shotgun"
            ? PlayerManager.Instance.shotgunShells
            : PlayerManager.Instance.bulletsInClip;

        // ===========================
        //  FIRE BUTTON PRESSED
        // ===========================
        if (EventSystem.current.IsPointerOverGameObject(pointerID) &&
            EventSystem.current.currentSelectedGameObject != null)
        {
            if (EventSystem.current.currentSelectedGameObject.gameObject.name == "Fire Button")
            {
                if (currentAmmo <= 0)
                    return;

                Firing = true;
            }
        }

        // ===========================
        //  FIRE BUTTON RELEASED
        // ===========================
        if (Input.GetMouseButtonUp(0))
        {
            Firing = false;
        }

        // ===========================
        //  OUT OF AMMO
        // ===========================
        if (currentAmmo <= 0)
        {
            Debug.Log("outofammo");
            Firing = false;
        }

        // ===========================
        //  UPDATE ANIMATOR
        // ===========================
        _animator.SetBool("Firegun", Firing);

    }
}








