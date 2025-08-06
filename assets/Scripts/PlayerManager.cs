using System;// Imports standard C# functionalities.
using UnityEngine;// Imports Unity’s core functions (GameObjects, Components, Physics, Scene Management).

public class PlayerManager : MonoBehaviour// 🔄 Manages the player's health, ammo, and game status.
{
    public static PlayerManager Instance;// Creates a Singleton instance of PlayerManager → Ensures only ONE PlayerManager exists globally.

    // 🔄 Player health variables.
    public float PlayerHealth;
    public float curHealth;
    // 🔄 Player lives counter.
    public int lives;
    // 🔄 Ammo variables for Twin Turbos and Shotgun.
    public int bulletsInClip;
    public int totalBullets = 20;// 🔄 Total bullets available in the game for Twin Turbos (default 20).

    public int shotgunShells;
    public int remainingBullets;
    public int remainingShotgunShells;

    public int numberOfGrenades;// Stores the number of regular grenades the player has.
    public int numberOfPhosphorusGrenades;// Stores the number of phosphorus grenades the player has.








    // This function likely activates or deactivates the player, but it's currently empty.
    public void Activate(bool activate)
    {
        // ❌ No code inside yet → Could be used later for toggling player visibility or enabling/disabling movement.
    }

    // 🔄 Runs once when the scene starts → Initializes health UI.
    private void Start()
    {
        // Updates the health bar UI to reflect the player's current health and number of lives.
        GameManager.UIManager.UpdateHealthBar(curHealth / PlayerHealth, lives);
    }

    // 🔄 Handles damage taken by the player → Updates health & triggers respawn or game over.
    public void TakeDamage(int damage)
    {
        // Reduces the player's health when taking damage.
        curHealth -= damage;

        // If health reaches zero, the player loses a life.
        if (curHealth <= 0)
        {
            lives--; // Decreases the player's life count.

            // If the player still has lives left, restore their health.
            if (lives >= 0)
            {
                RefreshHealth(); // Resets health to full (player respawns).
            }
            else
            {
                GameManager.UIManager.GameOver(); // No lives left → Calls Game Over function.
                return; // Stops further execution.
            }
        }

        // Updates the health bar UI to reflect the damage taken.
        GameManager.UIManager.UpdateHealthBar(curHealth / PlayerHealth, lives);
    }

    // 🔄 Refreshes health to full when the player respawns.
    private void RefreshHealth()
    {
        curHealth = PlayerHealth; // Resets health back to max value.
    }



    // 🔄 Handles player death → Stops music, loads the Game Over scene, and removes the player.
    public void Die()
    {
        AudioManager.Instance.StopAllMusic(); // Stops all background music upon death.

        GameManager.Instance.OpenScene(0); // Loads Scene 0 (likely Main Menu or Game Over screen).

        Destroy(this); // Removes PlayerManager from the scene to reset gameplay.
    }









    public WeaponSwitcher WeaponSwitcher { get; private set; }// Declares a WeaponSwitcher property → Manages weapon switching logic.
                                                              // 🔄 Uses `private set;` → Prevents other scripts from modifying it directly.

    private void Awake()/// 🔄 Runs when the PlayerManager script is loaded → Initializes the Singleton & Player Stats.
    {
        if (Instance == null)// Checks if a PlayerManager instance already exists.
        {
            Instance = this;// Sets this object as the global PlayerManager instance.
            DontDestroyOnLoad(gameObject);// Deletes duplicate instances to prevent conflicts.
        }
        else
        {
            Destroy(gameObject);
        }

        if (curHealth == 0)// If current health is uninitialized (0), set it to full health.
        {
            curHealth = PlayerHealth;
        }

        if (lives == 0)// If lives are uninitialized (0), start with 3 lives.
        {
            lives = 3;
        }








       
        WeaponSwitcher = GetComponent<WeaponSwitcher>();// Finds and stores a reference to the WeaponSwitcher component attached to the player.
                                                        // This allows PlayerManager to control weapon switching.
        if (WeaponSwitcher == null)// Checks if WeaponSwitcher was successfully assigned. If not, logs an error.
                                   // If WeaponSwitcher isn't found, this will display an error in the Unity console, helping you debug missing references.
        {
            Debug.LogError("WeaponSwitcher component not found on PlayerManager.");
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //bulletsInClip = 10;// Sets the initial clip size for Twin Turbos to 10 bullets.
                           // This ensures the player starts with 10 bullets in their weapon when picking it up.(MAYBE DO NOT NEED THIS LINE AND DELETE IT)

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        remainingBullets = totalBullets - (bulletsInClip * 2);// Calculates the remaining bullets based on total ammo.
                                          // The logic subtracts twice the clip size from the total bullets to determine how many bullets remain.
                                          // This likely accounts for reload mechanics or available reserves after equipping.
    }

    public void AddAmmo(string ammoType, int ammoToAdd)// 🔄 Handles ammo pickups, adding bullets or shotgun shells to the player's inventory.
    {
        Debug.Log($"{ammoType} {ammoToAdd} reloadingammo");// Logs the type of ammo being added and the amount in the Unity console.
                                                           // This helps with debugging to see if the correct ammo is being picked up.

        switch (ammoType)// Uses a switch statement to determine which type of ammo is being updated.
        {
            case "TwinTurbos":// Adds the newly acquired bullets to the player's remaining Twin Turbos ammo count.

                remainingBullets += ammoToAdd;// Updates the UI to show the correct bullet count after the player reloads.
                GameManager.UIManager.ReloadGun(ammoType, bulletsInClip, remainingBullets);
                break;
            case "Shotgun":// Adds the newly acquired shotgun shells to the player’s remaining shotgun ammo count.
                remainingShotgunShells += ammoToAdd;
                GameManager.UIManager.ReloadGun(ammoType, shotgunShells, remainingShotgunShells);// Updates the UI to show the correct shotgun shell count after reloading.
                break;
        }
       
    }







    internal void SetBulletsInClip(string weaponType, int clipSize)// Updates the number of bullets in a weapon clip before reloading.
                                                                   // `internal` means this function can only be accessed within the same **assembly** (not from other scripts outside the project).
    {

        switch (weaponType)// Uses a switch statement to determine which weapon's clip size needs updating.
        {
            case "TwinTurbos": // 🔫 Case 1: The player is using Twin Turbos.
                bulletsInClip = clipSize;// Updates the clip size for Twin Turbos pistols → The number of bullets inside the weapon after a reload.
                break;

            case "Shotgun":  // 🔫 Case 2: The player is using the Shotgun.
                shotgunShells = clipSize;// Updates the clip size for the Shotgun → The number of shells inside the weapon after a reload.

                break;
                default:// ❌ Default case: If the weaponType doesn’t match Twin Turbos or Shotgun.
                Debug.Log("don't have a ammo assigned"); // Logs an error message in Unity’s console → Helps debug incorrect weapon assignments.
                return;// Stops the function execution immediately → Prevents unintended changes if an incorrect weapon type is passed.
        }
    }
}


// PlayerManager script is a Singleton

///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

// ===================== SINGLETON SYSTEM SUMMARY =====================
// Your game has four key singletons:

// GameManager → Handles scene transitions, UI updates, and game state persistence.
// PlayerController → Manages FPS mechanics, ensuring only ONE active player exists per scene.
// PlayerManager → Stores player stats (health, ammo, lives, and weapons), keeping them persistent across scenes.
// SceneController → Oversees scene-specific details like music, player setup, and weapon assignments.
//
// The Singleton pattern ensures only ONE instance of each exists at any time.
// Prevents duplicate objects from interfering with game mechanics.
// Allows global access via GameManager.Instance, PlayerController.Instance, PlayerManager.Instance, and SceneController.Instance.

// Think of these singletons as the backbone of your game, keeping everything stable 






