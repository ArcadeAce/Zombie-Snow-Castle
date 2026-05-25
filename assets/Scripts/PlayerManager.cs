using System;
using System.Diagnostics;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    //  means other scripts are allowed to use it.
    //  means “there is only ONE of these in the entire game.” If something is static: it does not belong on a gameobect, it does not get destroyed when scenes change, it does not get duplicated, it exists before the scene load.
    //  the one and only PlayerManager in your entire game.// Singleton pattern to ensure only one instance exists.

    // 🔫 Weapon memory
    public bool hasShotgun; // Tracks if the player has acquired the shotgun, allowing for weapon switching and inventory management.The flag that turns TRUE only after you pick up the shotgun dropped by the Grenade Throwing Zombie Boss in Stage 3.
    public static string lastHeldWeaponType = "TwinTurbos";
    public string selectedWeaponType = "TwinTurbos";

    // ❤️ Health and lives
    public float PlayerHealth = 100f;// PlayerManager script needs these public variables to exist, and they need to be public, because they are the live memory that gets updated as the player moves through every scene, even though the amounts are different in the Player Manager script Inpsctor Panel boxes in the start scene.
    public float curHealth;
    public int lives = 3;

    // 🔫 Ammo tracking
    public int bulletsInClip;// PlayerManager script needs these public variables to exist, and they need to be public, because they are the live memory that gets updated as the player moves through every scene.
    public int totalBullets = 20;
    public int shotgunShells;
    public int remainingBullets;
    public int remainingShotgunShells;

    // 💣 Grenades
    public int numberOfGrenades;// PlayerManager script needs these public variables to exist, and they need to be public, because they are the live memory that gets updated as the player moves through every scene.
    public int numberOfPhosphorusGrenades;






    // 🔄 Weapon system reference
    public WeaponSwitcher WeaponSwitcher { get; private set; }// public allows other scripts to access the WeaponSwitcher reference, while private set means only PlayerManager can assign it. This ensures controlled access to the WeaponSwitcher component, allowing other scripts to use it without directly modifying the reference.
                                                              // get allows other scripts to read/access the current value of WeaponSwitcher, while private set means only PlayerManager can assign it. This ensures controlled access to the WeaponSwitcher component, allowing other scripts to use it without directly modifying the reference.
                                                              // set allows PlayerManager to assign the WeaponSwitcher reference, while get allows other scripts to access it without modifying it. This encapsulation ensures that the WeaponSwitcher component is properly initialized and managed by PlayerManager, while still being accessible for weapon-related functions across the game.

    private void Awake()//private void awake means that this function is only accessible within the PlayerManager script, and it runs when the script is first loaded. 

    {
        if (Instance == null)// checks if there is already an instance of PlayerManager in the game. If there isn’t, it assigns the current PlayerManager to Instance, making it the official reference for the entire game. Now, other scripts can access PlayerManager by calling PlayerManager.Instance instead of searching for it in the scene.
        {
            Instance = this;// this checks if there is already an instance of PlayerManager in the game. If there isn’t, it assigns the current PlayerManager to Instance, making it the official reference for the entire game. Now, other scripts can access PlayerManager by calling PlayerManager.Instance instead of searching for it in the scene.
            DontDestroyOnLoad(gameObject);// don't destroy on load means that this PlayerManager will persist across all scenes. This is crucial for maintaining player stats, weapon inventory, and game state as the player progresses through different levels. Without this line, a new PlayerManager would be created in each scene, causing loss of data and inconsistencies in gameplay. By using DontDestroyOnLoad, you ensure that the player's health, ammo, lives, and weapon status remain intact throughout the entire game experience.
        }
        else
        {
            Destroy(gameObject);// destroy checks if there is already an instance of PlayerManager in the game. If there is, it destroys the new one to prevent duplicates. This ensures that only one PlayerManager exists at any time, maintaining a consistent reference for player stats and game state across all scenes.
            return; // what return does is it exits the Awake() function early if a duplicate PlayerManager is found and destroyed. This prevents any further initialization or code execution in the new instance, ensuring that only the original PlayerManager continues to manage player stats and game state throughout the game.
        }

        // ✅ Weapon memory functions
        curHealth = PlayerHealth;// cur health means that the current health of the player is set to the maximum health defined in PlayerHealth. This initializes the player's health at the start of the game or when they respawn, ensuring they begin with full health.
        lives = lives == 0 ? 3 : lives;// lives means that if the player's lives are currently set to 0, it will reset them to 3. This is useful for initializing the player's lives at the start of the game or when they respawn, ensuring they have a standard number of lives to begin with. If lives are already above 0, it keeps the existing value, allowing for persistence across scenes.

        WeaponSwitcher = GetComponent<WeaponSwitcher>();// This means that the PlayerManager script is trying to find and assign the WeaponSwitcher component that is attached to the same GameObject. This allows PlayerManager to access and manage weapon switching functionality through the WeaponSwitcher reference. If the WeaponSwitcher component is not found, it will log an error message to help identify the issue during development.
        if (WeaponSwitcher == null)
        {
            //Debug.LogError("WeaponSwitcher component not found on PlayerManager.");
        }

        remainingBullets = totalBullets - (bulletsInClip * 2);// This line means that the remaining bullets for the Twin Turbos are calculated by taking the total bullets available and subtracting the bullets currently in both clips (assuming two clips for the Twin Turbos). This initializes the ammo count for the player, ensuring that the UI can accurately reflect how many bullets are left after accounting for what's currently loaded in the weapon.
    }


    private void Start()// private void start means that this function is only accessible within the PlayerManager script, and it runs after Awake() when the game starts.
    {
        GameManager.UIManager.UpdateHealthBar(curHealth / PlayerHealth, lives);// means that the PlayerManager is calling the UpdateHealthBar function from the UIManager, which is accessed through the GameManager singleton. This updates the health bar UI to reflect the player's current health and lives at the start of the game. The health bar will show the proportion of current health to maximum health, and it will also display the number of lives remaining.
    }

    public void Activate(bool activate)// means that this function can be called from other scripts to activate or deactivate the player. The parameter "activate" is a boolean that determines whether to activate (true) or deactivate (false) the player. This function is currently reserved for future use, allowing for potential features like temporary invincibility, hiding the player during cutscenes, or managing player visibility in certain game states.
    {
        // Reserved for future use
    }

    public void TakeDamage(int damage)// public void TakeDamage(int damage) means that this function can be called from other scripts to apply damage to the player. The parameter "damage" is an integer that represents the amount of damage to be subtracted from the player's current health. This function handles the logic for reducing health, checking for player death, and updating the UI accordingly.
    {
        curHealth -= damage;// curHealth -= damage is the line that reduces the player's current health by the amount of damage taken. This updates the player's health status whenever they are hit by an enemy or take damage from any source in the game.

        if (curHealth <= 0)
        {
            lives--;

            if (lives >= 0)
            {
                RefreshHealth();
            }
            else
            {
                GameManager.UIManager.GameOver();// this line calls the GameOver function from the UIManager, which is accessed through the GameManager singleton. This triggers the game over sequence in the UI, indicating that the player has lost all their lives and the game has ended. After calling GameOver, the function
                return;
            }
        }

        GameManager.UIManager.UpdateHealthBar(curHealth / PlayerHealth, lives);// This line updates the health bar UI to reflect the player's current health and lives after taking damage. It calculates the proportion of current health to maximum health and updates the UI accordingly, while also displaying the number of lives remaining. This ensures that the player has visual feedback on their health status after being hit.
    }

    private void RefreshHealth() // when player loses a life, resets the player's health to full after losing a life.
    {                            // Keeps gameplay fast by instantly restoring health without showing a death screen.
        curHealth = PlayerHealth;
    }

    public void Die()
    {
        AudioManager.Instance.StopAllMusic();// this line is for
        GameManager.Instance.OpenScene(0);// Loads the main menu (scene 0) so the player can restart the game. Which is scene 0 in the Build Settings. When the player dies, it stops all music and transitions back to the main menu, allowing the player to restart or exit the game.
    Destroy(this);// This line is for destroying the PlayerManager instance when the player dies. This is important to ensure that when the player restarts the game, a new PlayerManager can be created without conflicts from the previous instance. It helps to reset the game state and allows for a fresh start when the player chooses to play again after dying.
}

    public void AddAmmo(string ammoType, int ammoToAdd)// This line is for adding ammo to the player's inventory.  // Checks which weapon the ammo pickup belongs to and updates the correct ammo values.

    {

        switch (ammoType)// When player picks up an ammo pickup, this switch statement checks the type of ammo being added (e.g., "TwinTurbos" or "Shotgun") and updates the corresponding ammo counts for the player. It also calls the ReloadGun function from the UIManager to update the ammo display in the UI, ensuring that the player has accurate information about their current ammo status after picking up ammo. It also refreshes the UI so the player immediately sees the new ammo amount.
{
            case "TwinTurbos":// this line checks if the ammo type being added is for the Twin Turbos. If it is, it adds the specified amount of ammo to the remainingBullets count for the Twin Turbos and updates the UI to reflect the new ammo status for that weapon.
        remainingBullets += ammoToAdd;// this line adds the specified amount of ammo (ammoToAdd) to the remainingBullets count for the Twin Turbos. This updates the player's ammo inventory for that weapon when they pick up an ammo pickup.
        GameManager.UIManager.ReloadGun(ammoType, bulletsInClip, remainingBullets);// this line calls the ReloadGun function from the UIManager, passing in the ammo type, the current bullets in the clip, and the remaining bullets. This updates the ammo display in the UI to reflect the new ammo status for the Twin Turbos after picking up ammo. It ensures that the player has accurate information about their current ammo count for that weapon.
        break;

            case "Shotgun":// Same as above but for shotgunammo.
                remainingShotgunShells += ammoToAdd;
                GameManager.UIManager.ReloadGun(ammoType, shotgunShells, remainingShotgunShells);
                break;
        }
    }

    internal void SetBulletsInClip(string weaponType, int clipSize)// Sets the number of bullets currently loaded into the weapon's clip after reloading. // Used by both Twin Turbos and Shotgun to update PlayerManager's ammo values.
    {
        switch (weaponType)    // Checks which weapon is being reloaded and sets the correct number of bullets
        {
            case "TwinTurbos":// Set how many bullets are now loaded into the Twin Turbos after reloading.
                bulletsInClip = clipSize;
                break;

            case "Shotgun":   // Set how many shells are now loaded into the Shotgun after reloading.
                shotgunShells = clipSize;
                break;

            default:   // If the weapon type doesn't match anything, do nothing and exit.

                return;// This line exits the function early if the weapon type doesn't match "TwinTurbos" or "Shotgun". This prevents any unintended changes to ammo counts for weapons that don't use these ammo types, ensuring that only valid weapon types are processed for updating the bullets in the clip.
        }
    }




    /////////////////////////////////////////////////////////////Copilot 
    public void SaveShotgunAmmo(int current, int total)// this line is for saving the current and total shotgun ammo when the player switches away from the shotgun. This allows the player to retain their ammo count for the shotgun when they switch back to it later, ensuring that they don't lose ammo when switching weapons. It updates the PlayerManager's shotgunShells and remainingShotgunShells variables with the current ammo status for the shotgun.
{
        shotgunShells = current;
        remainingShotgunShells = total;
    }
    
    //////////////////////////////////////////////////////////////////////
    



    // ✅ Weapon memory functions
    public void AcquireTwinTurbos()// this line is for when the player acquires the Twin Turbos weapon. It sets the selectedWeaponType and lastHeldWeaponType to "TwinTurbos", allowing the player to switch back to it later and ensuring that the game remembers that the player has acquired this weapon. This function can be called when the player picks up the Twin Turbos in the game, updating their weapon inventory and enabling them to use it in combat.
{
        selectedWeaponType = "TwinTurbos";// this line is for setting the currently selected weapon type to "TwinTurbos" when the player acquires it. This allows the player to immediately use the Twin Turbos after picking it up, and it also updates the game state to reflect that this weapon is now active in the player's inventory.
    lastHeldWeaponType = "TwinTurbos";// this line is for setting the last held weapon type to "TwinTurbos" when the player acquires it. This allows the game to remember that the player has acquired the Twin Turbos, enabling them to switch back to it later if they switch to another weapon. It helps maintain weapon memory and inventory management for the player as they progress through the game.
}

    public void AcquireShotgun()
    {
        hasShotgun = true;
        selectedWeaponType = "Shotgun";
        lastHeldWeaponType = "Shotgun";
    }

    public void AcquireKnife()
{ // Sets it as the current and last held weapon so the player can use it and switch back to it later.
    selectedWeaponType = "Knife";// when the knife is in unity and made and added to the game, this function will be called when the player acquires the knife. It sets the selectedWeaponType and lastHeldWeaponType to "Knife", allowing the player to switch back to it later and ensuring that the game remembers that the player has acquired this weapon. This function can be called when the player picks up the knife in the game, updating their weapon inventory and enabling them to use it in combat.
                                
    lastHeldWeaponType = "Knife";
    }

    public void AcquireBat()
// Called when the player picks up the Baseball Bat.
// Sets it as the current and last held weapon so the player can use it and switch back to it later.
{
    selectedWeaponType = "Bat";// when the baseball bat is made and added to the game, this function will be called when the player acquires the baseball bat. It sets the selectedWeaponType and lastHeldWeaponType to "Bat", allowing the player to switch back to it later and ensuring that the game remembers that the player has acquired this weapon. This function can be called when the player picks up the baseball bat in the game, updating their weapon inventory and enabling them to use it in combat.
    lastHeldWeaponType = "Bat";
    }

    public void SetActiveWeapon(string weaponType) // Sets the weapon the player is currently using.
// Also remembers it as the last held weapon so the player can switch back to it later.

{
    selectedWeaponType = weaponType;
        lastHeldWeaponType = weaponType;
    }
}


    // ===================== PLAYER MANAGER SUMMARY =====================
    // Player Manager script starts in the start scene in the Inspectror Panel, and is set to DontDestroyOnLoad, so it persists across all scenes.
    // It stores all player stats (health, ammo, lives) and weapon inventory, allowing for consistent player data across scenes.
    // It also provides functions for taking damage, dying, adding ammo, and acquiring weapons, ensuring smooth gameplay and player progression.
    // The PlayerManager script is a crucial component for managing the player's state and inventory throughout the game, ensuring that player stats and weapon information are maintained consistently as they progress through different levels and encounters.



