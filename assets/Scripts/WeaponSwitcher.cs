using System;
using System.Diagnostics;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance; // ✅ Singleton reference

    public bool hasShotgun; // Tracks if the shotgun has been picked up
    public static string lastHeldWeaponType = "TwinTurbos"; // Tracks which weapon was last held before scene switch
    public string selectedWeaponType = "TwinTurbos"; // Default weapon

    // 🔄 Player health variables
    public float PlayerHealth;
    public float curHealth;

    // 🔄 Player lives counter
    public int lives;

    // 🔄 Ammo variables
    public int bulletsInClip;
    public int totalBullets = 20;
    public int shotgunShells;
    public int remainingBullets;
    public int remainingShotgunShells;

    public int numberOfGrenades;
    public int numberOfPhosphorusGrenades;

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

        if (curHealth == 0)
        {
            curHealth = PlayerHealth;
        }

        if (lives == 0)
        {
            lives = 3;
        }

        WeaponSwitcher = GetComponent<WeaponSwitcher>();
        if (WeaponSwitcher == null)
        {
            UnityEngine.Debug.LogError("WeaponSwitcher component not found on PlayerManager.");
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
        UnityEngine.Debug.Log($"{ammoType} {ammoToAdd} reloadingammo");

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
                UnityEngine.Debug.Log("don't have a ammo assigned");
                return;
        }
    }
}
