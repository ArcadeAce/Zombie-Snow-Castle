using System;
using System.Diagnostics;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    // 🔫 Weapon memory
    public bool hasShotgun;
    public static string lastHeldWeaponType = "TwinTurbos";
    public string selectedWeaponType = "TwinTurbos";

    // ❤️ Health and lives
    public float PlayerHealth = 100f;
    public float curHealth;
    public int lives = 3;

    // 🔫 Ammo tracking
    public int bulletsInClip;
    public int totalBullets = 20;
    public int shotgunShells;
    public int remainingBullets;
    public int remainingShotgunShells;

    // 💣 Grenades
    public int numberOfGrenades;
    public int numberOfPhosphorusGrenades;

    // 🔄 Weapon system reference
    public WeaponSwitcher WeaponSwitcher { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // ✅ Weapon memory functions
        curHealth = PlayerHealth;
        lives = lives == 0 ? 3 : lives;

        WeaponSwitcher = GetComponent<WeaponSwitcher>();
        if (WeaponSwitcher == null)
        {
            //Debug.LogError("WeaponSwitcher component not found on PlayerManager.");
        }

        remainingBullets = totalBullets - (bulletsInClip * 2);
    }

    private void Start()
    {
        GameManager.UIManager.UpdateHealthBar(curHealth / PlayerHealth, lives);
    }

    public void Activate(bool activate)
    {
        // Reserved for future use
    }

    public void TakeDamage(int damage)
    {
        curHealth -= damage;

        if (curHealth <= 0)
        {
            lives--;

            if (lives >= 0)
            {
                RefreshHealth();
            }
            else
            {
                GameManager.UIManager.GameOver();
                return;
            }
        }

        GameManager.UIManager.UpdateHealthBar(curHealth / PlayerHealth, lives);
    }

    private void RefreshHealth()
    {
        curHealth = PlayerHealth;
    }

    public void Die()
    {
        AudioManager.Instance.StopAllMusic();
        GameManager.Instance.OpenScene(0);
        Destroy(this);
    }

    public void AddAmmo(string ammoType, int ammoToAdd)
    {
       

        switch (ammoType)
        {
            case "TwinTurbos":
                remainingBullets += ammoToAdd;
                GameManager.UIManager.ReloadGun(ammoType, bulletsInClip, remainingBullets);
                break;

            case "Shotgun":
                remainingShotgunShells += ammoToAdd;
                GameManager.UIManager.ReloadGun(ammoType, shotgunShells, remainingShotgunShells);
                break;
        }
    }

    internal void SetBulletsInClip(string weaponType, int clipSize)
    {
        switch (weaponType)
        {
            case "TwinTurbos":
                bulletsInClip = clipSize;
                break;

            case "Shotgun":
                shotgunShells = clipSize;
                break;

            default:
            
                return;
        }
    }

    // ✅ Weapon memory functions
    public void AcquireTwinTurbos()
    {
        selectedWeaponType = "TwinTurbos";
        lastHeldWeaponType = "TwinTurbos";
    }

    public void AcquireShotgun()
    {
        hasShotgun = true;
        selectedWeaponType = "Shotgun";
        lastHeldWeaponType = "Shotgun";
    }

    public void AcquireKnife()
    {
        selectedWeaponType = "Knife";
        lastHeldWeaponType = "Knife";
    }

    public void AcquireBat()
    {
        selectedWeaponType = "Bat";
        lastHeldWeaponType = "Bat";
    }

    public void SetActiveWeapon(string weaponType)
    {
        selectedWeaponType = weaponType;
        lastHeldWeaponType = weaponType;
    }
}








