using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class UIManager : MonoBehaviour
{
    public HealthBar HealthBar;
    public TextMeshProUGUI nametext;
    public GameObject pauseDisplay;
    public Button twinTurbosButton; // Button reference for Twin Turbos
    public Button shotgunButton; // Button reference for Shotgun
    public Button switchbutton; // Button reference for the active switch button
    public TextMeshProUGUI bulletsInClipText;
    public TextMeshProUGUI bulletsRemainingText;

    // Adding references to the relevant UI elements
    public TextMeshProUGUI AmmoValue;
    public TextMeshProUGUI Separator;
    public TextMeshProUGUI TotalBullets;
    public TextMeshProUGUI ShotgunCurrentBullets;
    public TextMeshProUGUI SeperatorForShotgunNumbers;
    public TextMeshProUGUI ShotgunTotalBullets;
   

    private bool gamePaused;

    public static object Instance { get; internal set; }

    private void Awake()
    {
        GameManager.UIManager = this;

        if (nametext)
        {
            nametext.enabled = false;
        }

        if (pauseDisplay)
        {
            pauseDisplay.SetActive(false);
        }

        twinTurbosButton.gameObject.SetActive(true);
        shotgunButton.gameObject.SetActive(false);
        switchbutton = twinTurbosButton; // Initialize with Twin Turbos button

        twinTurbosButton.onClick.AddListener(SwitchToShotgun);
        shotgunButton.onClick.AddListener(SwitchToTwinTurbos);
    }

    internal void UpdateShotgunShells(int shotgunShells)
    {
        ShotgunCurrentBullets.SetText(shotgunShells.ToString());
    }

    public void UpdateHealthBar(float value, int lives)
    {
        HealthBar.UpdateHealth(value, lives);
    }

    public void GameOver()
    {
        StartCoroutine(HealthBar._GameOver());
    }

    public void DisplayZombiename(string name)
    {
        nametext.SetText(name);
        nametext.enabled = true;
    }

    public void TurnNameOff()
    {
        nametext.enabled = false;
    }

    public void PauseGame()
    {
        if (gamePaused)
        {
            Time.timeScale = 0.0f;
            if (pauseDisplay)
            {
                pauseDisplay.SetActive(true);
            }
        }
        else
        {
            Time.timeScale = 1.0f;
            if (pauseDisplay)
            {
                pauseDisplay.SetActive(false);
            }
        }

        gamePaused = !gamePaused;
    }

    public void FireWeapon()
    {
        Debug.Log("Firinggun");
        PlayerManager.Instance.WeaponSwitcher.activeWeapon.Shoot();
    }

    public void UpdateBullets(int amount)
    {
        bulletsInClipText.SetText(amount.ToString());
    }

    public void ReloadGun(string weaponType, int bullets, int bulletsRemaining)
    {
        switch (weaponType)
        {
            case "TwinTurbos":
            bulletsInClipText.SetText(bullets.ToString());
        bulletsRemainingText.SetText(bulletsRemaining.ToString());
        break;
            case "Shotgun":
                ShotgunCurrentBullets.SetText(bullets.ToString());
                ShotgunTotalBullets.SetText(bulletsRemaining.ToString());
                break;
        }
        
    }
  
    public void SwitchToShotgun()
    {
        if (PlayerManager.Instance.WeaponSwitcher.slot2Occupied) // Ensure the player has picked up the shotgun
        {
            twinTurbosButton.gameObject.SetActive(false);
            shotgunButton.gameObject.SetActive(true);
            switchbutton = shotgunButton; // Update switchbutton reference
            PlayerManager.Instance.WeaponSwitcher.SwitchWeaponTo("Shotgun");

            // Update UI for Shotgun
            ShowShotgunUI();
        }
    }

    public void SwitchToTwinTurbos()
    {
        twinTurbosButton.gameObject.SetActive(true);
        shotgunButton.gameObject.SetActive(false);
        switchbutton = twinTurbosButton; // Update switchbutton reference
        PlayerManager.Instance.WeaponSwitcher.SwitchWeaponTo("TwinTurbos");

        // Update UI for Twin Turbos
        ShowTwinTurbosUI();
    }

    public void Reload()
    {
        PlayerManager.Instance.WeaponSwitcher.activeWeapon.Reload();
    }

    // Function to show Twin Turbos UI
    public void ShowTwinTurbosUI()
    {
        AmmoValue.gameObject.SetActive(true);
        Separator.gameObject.SetActive(true);
        TotalBullets.gameObject.SetActive(true);

        ShotgunCurrentBullets.gameObject.SetActive(false);
        ShotgunTotalBullets.gameObject.SetActive(false);
        SeperatorForShotgunNumbers.gameObject.SetActive(false);
    }

    // Function to show Shotgun UI
    public void ShowShotgunUI()
    {
        AmmoValue.gameObject.SetActive(false);
        Separator.gameObject.SetActive(false);
        TotalBullets.gameObject.SetActive(false);

        ShotgunCurrentBullets.gameObject.SetActive(true);
        ShotgunTotalBullets.gameObject.SetActive(true);
        SeperatorForShotgunNumbers.gameObject.SetActive(true);
    }
}





































