using UnityEngine;// using UnityEngine; → Gives access to Unity’s core features like GameObjects, Components, Physics, and Scene Management.
using TMPro;// using TMPro; → Imports TextMeshPro, allowing you to work with high-quality UI text elements (like ammo counters and boss names).
using UnityEngine.UI;// using UnityEngine.UI; → Grants access to UI elements like Buttons, Sliders, and Images.
using System;// using System; → Imports standard C# functions, including math operations, date/time handling, and general utilities.

public class UIManager : MonoBehaviour
{
    public HealthBar HealthBar;//First "HealthBar" → Represents the HealthBar class/type, which manages the UI health bar for the player. Second "HealthBar" → Is the name of the variable that holds the reference to the actual health bar component in the scene.
    public GameObject pauseDisplay;// For the Pause button

    public TextMeshProUGUI ForZombieBossNameCurrentlyFighting;// For the current zombie boss the player is fighting

    //Adding references to the relevant UI elements
    public TextMeshProUGUI TwinTurbosCurrentBullets;// For the Twin Turbos pistols bullets when fired
    public TextMeshProUGUI TwinTurbosTotalBullets;//The total bullets for the Twin Turbos pistols
    public TextMeshProUGUI ShotgunCurrentBullets;// For the shotgun current bullets displayed when fired
    public TextMeshProUGUI ShotgunTotalBullets;// For the shotgun total bullets to go up and down when reloading and or in the player picks up ammo

    public TextMeshProUGUI TwinTurbosBulletsGoingDownWhenFired;// Making the bullets go down for the twin turbos
    public TextMeshProUGUI ForTwinTurbosTotalBulletsRemainingToGoDown;// Making the remaining Twin Turbos bullets go down when the player reloads

    public Button twinTurbosButton; // Button reference for Twin Turbos
    public Button shotgunButton; // Button reference for Shotgun



    public static object Instance { get; internal set; }
    //Creates a global reference to UIManager → Other scripts (like GameManager or PlayerManager) can access UIManager without searching for it in the scene.
    // Uses static so only one instance exists → Instead of making multiple UIManagers, this ensures consistency in the game’s UI handling.
    //object means it stores ANY type of reference → But usually, this should store UIManager, not a general object.
    //internal set; means only UIManager can modify it → Other scripts can read UIManager(GameManager.UIManager), but only UIManager can change its value.

    private void Awake()// This function runs automatically when the scene starts.
    {
        GameManager.UIManager = this;// Links this UIManager to the GameManager, so other scripts can easily access UI updates.

        if (ForZombieBossNameCurrentlyFighting)// Checks if the boss name UI exists in the scene.
        {
            ForZombieBossNameCurrentlyFighting.enabled = false;// Hides the boss name until the player walks through the zombie boss trigger.

        }

        twinTurbosButton.gameObject.SetActive(true);// Weapon switch button for Twin Turbos (starts active since it's the default weapon)
        shotgunButton.gameObject.SetActive(false);// Weapon switch button for Shotgun (weapon switch button is inactive until the player picks up a shotgun)



        twinTurbosButton.onClick.AddListener(SwitchToShotgun);// When the Twin Turbos button is clicked, switch the player's weapon to the Shotgun.
        shotgunButton.onClick.AddListener(SwitchToTwinTurbos);// When the Shotgun button is clicked, switch the player's weapon back to Twin Turbos.
    }

    internal void UpdateShotgunShells(int shotgunShells)// Updates the shotgun ammo display to show the current number of shells left.
    {
        ShotgunCurrentBullets.SetText(shotgunShells.ToString());// Convert number to text and apply it to the UI.
    }



    // For the health bar

    public void UpdateHealthBar(float value, int lives)// Updates the health bar display when the player's health changes (e.g., after taking damage)
    {
        HealthBar.UpdateHealth(value, lives);
    }

    public void GameOver()// Triggers the Game Over sequence when the player's health reaches zero.

    {
        StartCoroutine(HealthBar._GameOver());// Starts the Game Over sequence when the player runs out of health.
    }



    // for the pause button

    private bool gamePaused;
    // private → Means only this script (UIManager) can use gamePaused. Other scripts can’t access or change it directly—this prevents unwanted changes. Keeps the pause logic controlled inside UIManager, ensuring clean code structure. Think of It Like a Password-Protected Setting → Only UIManager has control, keeping pause behavior predictable and secure.
    // Bool Tracks whether the game is paused → Starts as false (game is running) When the player pauses, it switches to true (game is frozen) When the player unpauses, it switches back to false (game resumes)
    // What is bool? bool (short for Boolean) is a data type in C# that can only have two possible values:true ✅ → Something is ON, active, or happening false ❌ → Something is OFF, inactive, or not happening
    public void PauseGame()
    {
        if (gamePaused) // Check if the game is currently paused.
        {
            Time.timeScale = 0.0f;// Stop all game movement and physics by setting time scale to 0 (paused state).
        }
        else
        {
            Time.timeScale = 1.0f;// Resume normal game speed by setting time scale back to 1 (unpaused state).
        }

        gamePaused = !gamePaused;// Toggle the gamePaused flag (true becomes false, false becomes true).
    }








    public void DisplayZombiename(string name)
    {
        ForZombieBossNameCurrentlyFighting.SetText(name);// Updates the UI text to display the zombie boss's name.
        ForZombieBossNameCurrentlyFighting.enabled = true;// Makes sure the zombie boss name becomes visible on the screen when the fight begins.
    }

    public void TurnNameOff()// Turns off the zombie boss's name display when the fight is over.
    {
        ForZombieBossNameCurrentlyFighting.enabled = false;// Setting enabled = false; hides the zombie boss's name from the UI, making sure it disappears after the fight ends.
    }





    // For gun firing

    public void FireWeapon()// // Handles firing the currently equipped weapon (Twin Turbos or Shotgun) when the fire button is pressed.
    {
        Debug.Log("Firing gun");// Logs "Firinggun" to the console (useful for debugging when the player shoots).
        PlayerManager.Instance.WeaponSwitcher.activeWeapon.Shoot();
        // `PlayerManager` → The **global manager** storing the player's stats, weapons, health, and ammo.`Instance` → Since `PlayerManager` is a **singleton**, this ensures **only one** PlayerManager exists throughout the game.`WeaponSwitcher` → This manages **weapon switching**, tracking which weapon the player is currently holding.`activeWeapon` → Stores the **currently equipped weapon**, making sure the player is firing the right gun.`Shoot()` → Calls the **Shoot() function** on the active weapon, making the player fire Twin Turbos or Shotgun.

        //  Think of It Like This:  PlayerManager is **your inventory hub** (it holds everything),  
        //Instance is **your access key**(making sure only one PlayerManager exists),  
        //WeaponSwitcher is **your gun holster**(knows which weapon is equipped),  
        //activeWeapon is **the gun in your hand**(ready to fire),  and Shoot() is **the trigger pull**(firing the bullet).
    }

    public void UpdateBullets(int amount)// Updates the displayed bullet count on the UI when the player shoots.
    {
        TwinTurbosBulletsGoingDownWhenFired.SetText(amount.ToString());// Converts the remaining bullet amount to a string and updates the UI text for Twin Turbos bullets.
    }





    // This function updates the UI to show the correct ammo count when reloading.
    // It changes both the **clip bullets** and **total remaining bullets** for Twin Turbos or Shotgun.
    public void ReloadGun(string weaponType, int bullets, int bulletsRemaining)
    {
        // This 'switch' checks which weapon is being reloaded.
        switch (weaponType)
        {
            // 🔫 Case 1: The player is reloading Twin Turbos.
            case "TwinTurbos":

                //  Updates the UI to show how many bullets are inside the Twin Turbos clip after reloading.
                TwinTurbosBulletsGoingDownWhenFired.SetText(bullets.ToString());

                //  Updates the UI to display the total remaining Twin Turbos bullets in the player's inventory.
                ForTwinTurbosTotalBulletsRemainingToGoDown.SetText(bulletsRemaining.ToString());

                // Break ends the case → It stops checking and prevents unnecessary executions.
                break;

            // 🔫 Case 2: The player is reloading the Shotgun.
            case "Shotgun":

                //  Updates the UI to show how many bullets are inside the Shotgun clip after reloading.
                ShotgunCurrentBullets.SetText(bullets.ToString());

                //  Updates the UI to display the total remaining shotgun shells in the player's inventory.
                ShotgunTotalBullets.SetText(bulletsRemaining.ToString());

                //  Break stops the execution for Shotgun reload logic.
                break;
        }
    }




    // 🔫 Switches the player's weapon to the Shotgun (if available)
    public void SwitchToShotgun()
    {
        // Checks if the player has picked up the shotgun before switching.
        if (PlayerManager.Instance.WeaponSwitcher.slot2Occupied)
        {
            // Hides the Twin Turbos button since Shotgun is being selected.
            twinTurbosButton.gameObject.SetActive(false);

            // Shows the Shotgun button so the player knows the weapon is active.
            shotgunButton.gameObject.SetActive(true);

            // Calls WeaponSwitcher to swap the equipped weapon to the Shotgun.
            PlayerManager.Instance.WeaponSwitcher.SwitchWeaponTo("Shotgun");

            // Updates the UI to reflect the Shotgun being selected.
            ShowShotgunUI();
        }
    }

    // 🔫 Switches the player's weapon back to Twin Turbos
    public void SwitchToTwinTurbos()
    {
        // Shows the Twin Turbos button so the player can switch back.
        twinTurbosButton.gameObject.SetActive(true);

        // Hides the Shotgun button since Twin Turbos is being selected.
        shotgunButton.gameObject.SetActive(false);

        // Calls WeaponSwitcher to swap the equipped weapon back to Twin Turbos.
        PlayerManager.Instance.WeaponSwitcher.SwitchWeaponTo("TwinTurbos");

        // Updates the UI to reflect Twin Turbos being selected.
        ShowTwinTurbosUI();
    }







    // 🔄 Handles reloading the currently equipped weapon
    public void Reload()
    {
        // ✅ Calls the Reload() function on the **active weapon** (Twin Turbos or Shotgun).
        PlayerManager.Instance.WeaponSwitcher.activeWeapon.Reload();
    }

    // 🔄 Updates the UI to show Twin Turbos ammo and hide Shotgun ammo
    public void ShowTwinTurbosUI()
    {
        // ✅ Makes the Twin Turbos ammo UI visible
        TwinTurbosCurrentBullets.gameObject.SetActive(true);
        TwinTurbosTotalBullets.gameObject.SetActive(true);

        // ❌ Hides the Shotgun ammo UI, since Twin Turbos is selected
        ShotgunCurrentBullets.gameObject.SetActive(false);
        ShotgunTotalBullets.gameObject.SetActive(false);
    }

    // 🔄 Updates the UI to show Shotgun ammo and hide Twin Turbos ammo
    public void ShowShotgunUI()
    {
        // ❌ Hides Twin Turbos ammo UI, since Shotgun is selected
        TwinTurbosCurrentBullets.gameObject.SetActive(false);
        TwinTurbosTotalBullets.gameObject.SetActive(false);

        // ✅ Makes the Shotgun ammo UI visible
        ShotgunCurrentBullets.gameObject.SetActive(true);
        ShotgunTotalBullets.gameObject.SetActive(true);
    }
}



// UIManager script is on the Canvas in all unity scenes
// UIManager is NOT a singleton because it's attached to the Canvas, and your Canvas exists in every scene—meaning a new UIManager instance is loaded when a scene changes
// The UIManager script does NOT handle firing itself—it only updates the ammo UI after a weapon is fired


// ===================== UI MANAGER SUMMARY =====================
// The UIManager script handles all UI-related updates, ensuring 
// the player sees accurate information during gameplay.
//
// 🔹 Core Responsibilities:
// ✅ Health & Lives Tracking → Updates the health bar and player lives when taking damage
// ✅ Weapon UI Management → Displays ammo counts for Twin Turbos & Shotgun, switching UI elements based on the equipped weapon
// ✅ Weapon Switching → Handles switching between Twin Turbos & Shotgun, ensuring the correct weapon UI is displayed
// ✅ Pause System → Toggles pause display, stopping or resuming game time
// ✅ Zombie Boss Name Display → Shows or hides the name of the zombie boss being fought
// ✅ Scene Integration → Communicates with GameManager to ensure UI values stay updated when transitioning between scenes
//
// ❌ What It Does NOT Do:
// ❌ Does NOT control firing directly → It only updates UI when a weapon fires, but actual shooting is handled by WeaponSwitcher & Weapon scripts
// ❌ Is NOT a singleton → Since UIManager is attached to the Canvas, a new instance is created in each scene instead of persisting globally
//
// Think of UIManager as the **visual backbone** of your game, ensuring the 
// player always has up-to-date ammo, health, and weapon information!


































