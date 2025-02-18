using System;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    public float PlayerHealth;
    public float curHealth;
    public int lives;
    public int bulletsInClip;
    public int shotgunShells;
    public int remainingBullets;
    public int remainingShotgunShells;
    public int totalBullets = 20;
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
        }

        if (curHealth == 0)
        {
            curHealth = PlayerHealth;
        }

        if (lives == 0)
        {
            lives = 3;
        }

        // Ensure WeaponSwitcher is assigned correctly
        WeaponSwitcher = GetComponent<WeaponSwitcher>();
        if (WeaponSwitcher == null)
        {
            Debug.LogError("WeaponSwitcher component not found on PlayerManager.");
        }

        bulletsInClip = 10;
        remainingBullets = totalBullets - (bulletsInClip * 2);
    }

    public void AddAmmo(string ammoType, int ammoToAdd)
    {
        Debug.Log($"{ammoType} {ammoToAdd} reloadingammo");

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

    public void Activate(bool activate)
    {
        // Logic to activate or deactivate the player
    }

    private void Start()
    {
        GameManager.UIManager.UpdateHealthBar(curHealth / PlayerHealth, lives);
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
                Debug.Log("don't have a ammo assigned");
                return;
        }
    }
}







