using StarterAssets;// This likely comes from a Unity package (like StarterAssets for FPS controllers). If your script isn’t calling anything from StarterAssets, Unity greys it out because it’s not in use.
using System.Collections;// Provides tools for working with basic collections like arrays and lists in C#. Includes classes like IEnumerator, which is used in coroutines (when you need timed or delayed execution). Allows scripts to loop through elements efficiently, even if you’re working with dynamic data.
using System.Collections.Generic;// Allows you to use advanced collection types like List<>, Dictionary<>, Queue<>, and HashSet<>. More powerful than standard arrays ([]) → These collections can resize dynamically and offer better performance for handling game data. Used often in inventory systems, enemy spawning, and tracking multiple objects dynamically.
using UnityEngine;

public class SceneController : MonoBehaviour
//Since your Canvas exists in every scene, the SceneController script is always active, ensuring smooth scene transitions and setups.
{
    public static SceneController Instance;// public makes it accessible from any other script → Meaning other scripts can use SceneController.Instance instead of manually finding it in the scene
                                           // Creates a static reference to SceneController → This ensures there is only one SceneController instance at all times.
                                           // SceneController Instance Acts as a Singleton → Prevents multiple SceneControllers from being created, avoiding conflicts. Lets other scripts easily access SceneController → Instead of manually finding it in the scene, they can simply call SceneController.Instance. Ensures consistent scene management → Keeps audio, player initialization, and scene logic stable across levels.
    void Start()// void means the function doesn’t return a value → It just executes tasks like setting up weapons, music, or initializing variables.
                // Start() is a built-in Unity function → It runs automatically when the scene loads.
    {
        if (Instance == null)//if → This checks a condition. Think of it like asking a Yes/No question before taking action.
                             //( & ) (Parentheses) → These hold the condition being checked. Everything inside them must evaluate to either true or false.
                             //Instance → This is a static variable representing the SceneController singleton. == → This means "Is equal to?". It’s checking whether Instance is currently set to null. null → This means "Nothing exists." If Instance is null, it means there is no active SceneController yet.
        {
            Instance = this;//Instance = this; → This assigns the current SceneController to the Instance variable. Now, other scripts can access it using SceneController.Instance, instead of searching for it in the scene manually. This is part of the Singleton pattern, ensuring only ONE SceneController exists at a time.
        }
        AudioManager.Instance.PlayMusic(Constants.CAVE_MUSIC);
        //AudioManager.Instance → Accesses the singleton instance of the AudioManager script (meaning it’s grabbing the one that already exists). .PlayMusic(Constants.CAVE_MUSIC); → Calls the PlayMusic function inside AudioManager, telling it to start playing the CAVE_MUSIC track. Constants.CAVE_MUSIC → Refers to a predefined music track stored in the Constants script or file.
        // Think of It Like This: Your SceneController is telling AudioManager: "Hey, start playing the cave music now!


        if (PlayerManager.Instance != null)// if → Checks a condition before running any code. Think of it like asking a YES or NO question before proceeding. ( & ) (Parentheses) → Hold the condition being checked—everything inside must result in either true or false. PlayerManager.Instance != null →
                     
        //Checks if the PlayerManager exists(Instance).
                                           //!= means "NOT equal to", so this line is asking: "Is there a PlayerManager currently active?"
                                           //If YES, move to the next check.
                                           //If NO, log an error(Debug.LogError("PlayerManager.Instance is not initialized.");).

        {
     



            if (PlayerManager.Instance.WeaponSwitcher != null)//if → This checks a condition before running any code. Think of it like asking a YES or NO question before proceeding. ( & ) (Parentheses) → Hold the condition being checked—everything inside must result in either true or false.  PlayerManager → Refers to the PlayerManager class, which manages the player’s health, ammo, lives, and weapons.
                                                              //. (Dot Operator) → Used to access variables or functions inside PlayerManager. Instance → A static reference to the one and only PlayerManager that exists in the game. . (Dot Operator Again) → Used to access WeaponSwitcher inside PlayerManager. WeaponSwitcher → This is the player’s weapon system, responsible for handling weapon changes and setups. != → Means "NOT equal to". null → Means "Nothing exists." What This Line Is Asking: "Is the WeaponSwitcher system available inside PlayerManager?"
            {
                Debug.Log("Setting up weapons...");//Debug.Log("Setting up weapons..."); → Prints a message in Unity's console → Helps confirm that the weapon setup process is starting. ➡️ Think of it like a status update before loading weapons.
                PlayerManager.Instance.WeaponSwitcher.SetupWeapons();// PlayerManager.Instance.WeaponSwitcher.SetupWeapons(); → Calls the SetupWeapons() function inside the WeaponSwitcher script. This ensures that the player's weapons are correctly set up after switching scenes. ➡️ If the player already has weapons assigned, it reloads them into the correct slots.
                PlayerManager.Instance.WeaponSwitcher.SyncWeaponVisibility();

      
            }
            else
            {
                Debug.LogError("WeaponSwitcher is not initialized.");// else { Debug.LogError("WeaponSwitcher is not initialized."); } → If WeaponSwitcher doesn’t exist, this logs an error.  Helps catch issues where weapons fail to initialize properly. ➡️ This is a safety check to make sure WeaponSwitcher is properly assigned in PlayerManager.
            }
        }
        else
        {
            Debug.LogError("PlayerManager.Instance is not initialized.");//else { Debug.LogError("PlayerManager.Instance is not initialized."); } → If PlayerManager.Instance is null, this logs another error. Prevents game-breaking bugs by confirming that PlayerManager is correctly loaded. ➡️ Think of this as a last checkpoint to ensure PlayerManager is working.
        }

    }
}

// Scene Controller script is a singleton
// The three singletons in your game—GameManager, PlayerController, and SceneController—cover the core aspects of scene transitions, player control, and scene initialization.
// Since PlayerManager.Instance is a singleton, it exists globally and doesn’t need to be manually placed in every scene.
// Just like GameManager, your PlayerManager is a singleton, meaning it doesn’t need to be attached to a GameObject in each scene. Instead, it exists globally and can be accessed anywhere using PlayerManager.Instance.
// Think of It Like This: GameManager & PlayerManager need singletons because they store global game data (health, UI, scene transitions). WeaponSwitcher is different—since it’s physically attached to the FPS Controller, it exists only when the player is in the scene, so there’s no need for a singleton.


// ===================== SCENE CONTROLLER SUMMARY =====================
// The SceneController script is responsible for setting up key scene elements 
// when transitioning into a new environment.
//
// 🔹 Uses the Singleton pattern → Ensures only ONE SceneController exists at all times.
// 🔹 Initializes background music → Calls AudioManager to start scene-specific music.
// 🔹 Verifies PlayerManager exists → Ensures player stats (health, ammo, lives) are ready.
// 🔹 Verifies WeaponSwitcher exists → Ensures the player's weapons are correctly set up.
// 🔹 Handles game state consistency → Ensures essential player components load properly.
// 🔹 Logs errors if PlayerManager or WeaponSwitcher are missing → Helps with debugging and preventing broken mechanics.
//
// Think of SceneController as the **game’s scene coordinator**, making sure music starts, 
// player stats load, and weapons are properly set up before gameplay begins! 


/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

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
//
// Think of these singletons as the backbone of your game, keeping everything stable 


