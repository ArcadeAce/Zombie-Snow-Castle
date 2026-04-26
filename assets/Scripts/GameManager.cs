using UnityEngine;



using UnityEngine.SceneManagement;// this line allows you to use the SceneManager class, which is essential for loading and managing scenes in Unity. It provides functions like LoadScene() to switch between different game levels or menus.

// Build Settings is where you list available scenes in your game and assign them an index. When you call SceneManager.LoadScene(index), Unity looks up that index in Build Settings to determine which scene to load. For example, if you have a Main Menu scene at index 0 and a Level 1 scene at index 1, calling LoadScene(1) will switch to Level 1. This is how your GameManager controls scene transitions based on the scene index defined in Build Settings.
// Your GameManager uses Scene Manager functions to swap scenes based on the scene index stored in Build Settings.

public class GameManager : MonoBehaviour// this line is declaring a new class called GameManager that inherits from MonoBehaviour, which is the base class for all Unity scripts. By inheriting from MonoBehaviour, GameManager can be attached to a GameObject in the Unity scene and can use Unity's event functions like Awake(), Start(), Update(), etc. This allows GameManager to control game logic, manage scenes, and interact with other components in the game.
                                       // MonoBehaviour is like Unity’s "passport"—any script that inherits from it can access Unity’s core features, like physics, scenes, input, and triggers!

{
    public static GameManager Instance;
    // Creates a single global GameManager that any script can access.
    // This ensures there is only one GameManager in the entire game (singleton pattern).

    public static UIManager UIManager { get; set; }
    // Creates a global reference to the UIManager so any script can access the game's UI.
    // The UIManager is stored here so GameManager can update UI elements from anywhere.

    private void Awake() // Runs before the game starts and sets up the GameManager singleton.
{
          if (Instance == null)
        {
            Instance = this;// Makes this the one and only GameManager that all scripts can access
            DontDestroyOnLoad(gameObject); // Keeps the GameManager alive when switching scenes so it never gets destroyed.
        }
        else
        {
            Destroy(gameObject); // Prevents duplicates
        }
    } // <-- closes Awake() properly


    public void OpenScene(int index)
// Loads a new scene using its Build Settings index.
// This is how the game switches levels, loads the main menu, or restarts the game.
{
    SceneManager.LoadScene(index);
    // Loads the scene that matches this index number from the Build Settings.
    // This instantly switches the game to a new level, menu, or boss room.

}
    public void QuitGame()// Closes the game when the player chooses the Quit option from the main menu.
{
        Application.Quit(); 
    }
}



// ===================== GAME MANAGER SUMMARY =====================
// The GameMananager is a singleton
// GameManager is a global singleton that persists across all scenes.
// It provides a single place for the game to handle scene transitions,
// quitting the game, and storing a global reference to the UIManager.
//
// Because it uses DontDestroyOnLoad, only one GameManager exists for the entire game, and all scripts can access it through GameManager.Instance.
// 
// GameManager controls:
// - Loading new scenes using their Build Settings index
// - Returning to the main menu or restarting the game
// - Quitting the application on Android/PC builds
// - Providing global access to the UIManager for UI updates
//
// This script acts as the central controller for scene flow and UI access,
// ensuring smooth transitions and consistent game behavior across all levels.


////////////


// ===================== SINGLETON SYSTEM SUMMARY =====================
// Your game has four key singletons:
// GameManager → Handles scene transitions, UI updates, and game state persistence
// PlayerController → Manages FPS mechanics, ensuring only ONE active player exists per scene
// PlayerManager → Stores player stats (health, ammo, lives, and weapons), keeping them persistent across scenes
// SceneController → Oversees scene-specific details like music, player setup, and weapon assignments
//
// The Singleton pattern ensures only ONE instance of each exists at any time
// Prevents duplicate objects from interfering with game mechanics
// Allows GLOBAL = means any script can access it from anywhere and access via GameManager.Instance, PlayerController.Instance, PlayerManager.Instance, and SceneController.Instance
//
// Think of these singletons as the backbone of your game, keeping everything stable 

