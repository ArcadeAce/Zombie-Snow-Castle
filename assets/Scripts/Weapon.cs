using System;

using UnityEngine;
using UnityEngine.EventSystems;

public class Weapon : MonoBehaviour
{
    public int damage;
    public int clipSize;
    public int totalBullets;
    protected Transform fpsCam;

    public int index;
    public bool Active;
    public bool chestweapon;

    [HideInInspector] public Animator _animator;

    private bool Firing;

    public string weaponType;

    internal int weaponIndex;

    // ✅ The ONLY Shoot() method — Twin Turbos base logic
    public virtual void Shoot()
    {
        PlayerManager.Instance.bulletsInClip--;
        if (PlayerManager.Instance.bulletsInClip < 0)
            PlayerManager.Instance.bulletsInClip = 0;

        GameManager.UIManager.UpdateBullets(PlayerManager.Instance.bulletsInClip);

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.position, fpsCam.forward, out hit, 1000f))
        {
            if (hit.collider && hit.collider.transform.TryGetComponent(out Enemy enemy))
            {
                enemy.TakeDamage(damage, hit);
            }
            else if (hit.collider.transform.parent != null)
            {
                if (hit.collider.transform.parent.TryGetComponent(out Enemy enemy2))
                {
                    enemy2.TakeDamage(damage, hit);
                }
            }
        }
    }
    /// ///////////////////////////////Copilot for the shotgun 5 - 0 problem the old weapon script is in discord

    private void OnEnable()
    {
        _animator = GetComponent<Animator>();
        fpsCam = PlayerController.Instance.cam.transform;

        Active = true;

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
    /// /////////////////////////////////


    public virtual void Start()
    {
        // Intentionally empty — child classes override if needed
    }

    public void Setup(bool chest)
    {
        if (chest)
        {
            // Optional chest logic (currently disabled)
        }

        GameManager.UIManager.ReloadGun(
            weaponType,
            PlayerManager.Instance.bulletsInClip,
            PlayerManager.Instance.remainingBullets
        );
    }

    internal void Drop()
    {
        // Placeholder for future weapon drop logic
    }

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

    private void Update()
    {
        Int32 pointerID;

#if UNITY_EDITOR
        pointerID = -1;
#else
        pointerID = 0;
#endif

        // Fire button pressed
        {
            if (EventSystem.current.IsPointerOverGameObject(pointerID) &&
                EventSystem.current.currentSelectedGameObject != null)
            {
                if (EventSystem.current.currentSelectedGameObject.gameObject.name == "Fire Button")
                {
                    if (PlayerManager.Instance.bulletsInClip <= 0)
                        return;

                    Firing = true;
                }
            }
        }

        // Fire button released
        if (Input.GetMouseButtonUp(0))
        {
            Firing = false;
        }

        // Out of ammo
        if (PlayerManager.Instance.bulletsInClip <= 0)
        {
            Debug.Log("outofammo");
            Firing = false;
        }

        _animator.SetBool("Firegun", Firing);
    }
}







