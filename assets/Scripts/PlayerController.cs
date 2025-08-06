using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Transform WeaponHolder; // public Transform WeaponHolder; → Holds the location where weapons are attached to the player, ensuring guns appear in the correct place when equipped.
    public Camera cam; // public Camera cam; → Stores a reference to the player's camera, ensuring the correct viewpoint and aiming mechanics for shooting.
    
    public static PlayerController Instance// public static PlayerController Instance; → This creates a global reference to PlayerController, allowing other scripts to access it
    {
        get;// get; → Allows other scripts to read the current PlayerController instance.
        private set;// private set; → Prevents other scripts from modifying the instance—only this script can set it.
    }

    private void Awake()
    {
        Debug.Log("Player Controller" + SceneManager.GetActiveScene().name);
        // If there is an instance, and it's not me, delete myself.
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);//
        }
    }
}
// Player Controller is a Singleton
// The three singletons in your game—GameManager, PlayerController, and SceneController—cover the core aspects of scene transitions, player control, and scene initialization.
// Why Is PlayerController a Singleton? public static PlayerController Instance { get; private set; } → This ensures only ONE PlayerController instance exists across the game. if (Instance != null && Instance != this) Destroy(gameObject); → If another PlayerController exists, it removes duplicates, keeping only one. Instance = this; → If no existing instance is found, this PlayerController becomes the active instance.
// How Does This Work with Unity Starter Assets FPS Controller? Attached to the FPS Controller (Parent Object) → Because the FPS Controller is the player itself, PlayerController exists only when the player is instantiated in a scene. Not Using DontDestroyOnLoad(gameObject); → Unlike GameManager, PlayerController does NOT persist across scenes, meaning each level gets a fresh FPS Controller instance. Each Scene Has a Specific Player Spawn Point → The PlayerController only exists where the player is placed, ensuring correct positioning in each level.

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


