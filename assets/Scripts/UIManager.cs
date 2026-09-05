using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public HealthBar HealthBar;
    public GameObject pauseDisplay;

    public TextMeshProUGUI ForZombieBossNameCurrentlyFighting;

    public TextMeshProUGUI TwinTurbosCurrentBullets;
    public TextMeshProUGUI TwinTurbosTotalBullets;
    public TextMeshProUGUI ShotgunCurrentBullets;
    public TextMeshProUGUI ShotgunTotalBullets;

    public TextMeshProUGUI TwinTurbosBulletsGoingDownWhenFired;
    public TextMeshProUGUI ForTwinTurbosTotalBulletsRemainingToGoDown;

    public Button twinTurbosButton;
    public Button shotgunButton;

    public GameObject lookSensitivityPanel;

    // ⭐ Charge‑Up Button reference
    public GameObject chargeUpButton;

    public static object Instance { get; internal set; }

    private void Awake()
    {
        GameManager.UIManager = this;

        if (ForZombieBossNameCurrentlyFighting)
            ForZombieBossNameCurrentlyFighting.enabled = false;

        // Turn Charge‑Up Button OFF at scene start
        if (chargeUpButton != null)
            chargeUpButton.SetActive(false);

        switch (PlayerManager.Instance.selectedWeaponType)
        {
            case "TwinTurbos":
                SwitchToTwinTurbos();
                break;

            case "Shotgun":
                SwitchToShotgun();

                if (chargeUpButton != null)
                    chargeUpButton.SetActive(true);
                break;

            default:
                SwitchToTwinTurbos();
                break;
        }

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

    private bool gamePaused;

    public void PauseGame()
    {
        gamePaused = !gamePaused;

        Time.timeScale = gamePaused ? 0f : 1f;

        lookSensitivityPanel.SetActive(gamePaused);
    }

    public void DisplayZombiename(string name)
    {
        ForZombieBossNameCurrentlyFighting.SetText(name);
        ForZombieBossNameCurrentlyFighting.enabled = true;
    }

    public void TurnNameOff()
    {
        ForZombieBossNameCurrentlyFighting.enabled = false;
    }

    public void FireWeapon()
    {
        PlayerManager.Instance.WeaponSwitcher.activeWeapon.Shoot();
    }

    public void UpdateBullets(int amount)
    {
        TwinTurbosBulletsGoingDownWhenFired.SetText(amount.ToString());
    }

    public void ReloadGun(string weaponType, int bullets, int bulletsRemaining)
    {
        switch (weaponType)
        {
            case "TwinTurbos":
                TwinTurbosBulletsGoingDownWhenFired.SetText(bullets.ToString());
                ForTwinTurbosTotalBulletsRemainingToGoDown.SetText(bulletsRemaining.ToString());
                break;

            case "Shotgun":
                ShotgunCurrentBullets.SetText(bullets.ToString());
                ShotgunTotalBullets.SetText(bulletsRemaining.ToString());
                break;
        }
    }

    public void SwitchToShotgun()
    {
        if (PlayerManager.Instance.WeaponSwitcher.slot2Occupied)
        {
            twinTurbosButton.gameObject.SetActive(false);
            shotgunButton.gameObject.SetActive(true);

            PlayerManager.Instance.WeaponSwitcher.SwitchWeaponTo("Shotgun");

            ShowShotgunUI();
            ShowChargeUpButton();
        }
    }

    public void SwitchToTwinTurbos()
    {
        twinTurbosButton.gameObject.SetActive(true);
        shotgunButton.gameObject.SetActive(false);

        PlayerManager.Instance.WeaponSwitcher.SwitchWeaponTo("TwinTurbos");

        ShowTwinTurbosUI();
        HideChargeUpButton();
    }

    public void Reload()
    {
        PlayerManager.Instance.WeaponSwitcher.activeWeapon.Reload();
    }

    public void ShowTwinTurbosUI()
    {
        TwinTurbosCurrentBullets.gameObject.SetActive(true);
        TwinTurbosTotalBullets.gameObject.SetActive(true);

        ShotgunCurrentBullets.gameObject.SetActive(false);
        ShotgunTotalBullets.gameObject.SetActive(false);
    }

    public void ShowShotgunUI()
    {
        TwinTurbosCurrentBullets.gameObject.SetActive(false);
        TwinTurbosTotalBullets.gameObject.SetActive(false);

        ShotgunCurrentBullets.gameObject.SetActive(true);
        ShotgunTotalBullets.gameObject.SetActive(true);
    }

    public void ShowChargeUpButton()
    {
        if (chargeUpButton != null)
            chargeUpButton.SetActive(true);
    }

    public void HideChargeUpButton()
    {
        if (chargeUpButton != null)
            chargeUpButton.SetActive(false);
    }

    // ⭐ NEW — Charge‑Up Button OnClick handler
    public void OnChargeUpPressed()
    {
        if (PlayerManager.Instance.WeaponSwitcher.activeWeapon is Shotgun shotgun)
        {
            shotgun.SpinBarrel();   // This rotates the barrel –70° on Z
        }
    }
}



































