using UnityEngine; // Importing the UnityEngine namespace to access Unity's core functionality.
using UnityEngine.SceneManagement;
// Importing the UnityEngine.SceneManagement namespace to work with scene management.
// Build Settings is where you list available scenes and assign them index numbers → Scene Manager uses those index numbers to load scenes correctly. 
// Your GameManager uses Scene Manager functions to swap scenes based on the scene index stored in Build Settings.

public class GameManager : MonoBehaviour // Declaring a public class named GameManager, inheriting from MonoBehaviour.
                                         // MonoBehaviour is like Unity’s "passport"—any script that inherits from it can access Unity’s core features, like physics, scenes, input, and triggers!

{
    public static GameManager Instance;
    // Declaring a public static variable named Instance to hold a reference to the GameManager instance
    // Think of static like this: static is like a global radio frequency—all scripts can tune in and use it without needing a separate copy!
    // Declaring public static properties to provide global access to UIManager and PlayerController instances

    // Instead of needing a new GameManager, other scripts can simply call GameManager.Instance globally! Used for singleton patterns → Ensures only ONE GameManager exists, controlling scene transitions, player deaths, and more

    // What Instance Does: Stores a reference to the GameManager → Other scripts can call GameManager. Instance instead of creating new GameManagers.Ensures only ONE GameManager exists → Prevents duplicate GameManagers from interfering with scene transitions and player mechanics
    public static UIManager UIManager { get; set; }
    // Public allows all scripts to access and modify the variable/property → Without public, only the script itself could use it.
    // What static Does: Makes UIManager belong to the GameManager class itself → Instead of being tied to an individual instance, it exists globally. Ensures there’s only ONE UIManager reference → Meaning all scripts access the same UIManager rather than creating multiple instances
    // The first UIManager → Refers to the type (the class UIManager). This tells Unity that this property stores a reference to a UIManager object. The second UIManager → Is the name of the property itself. This is how other scripts will call and interact with UIManager
    // What get; set; does get → Allows other scripts to read/access the current value of UIManager. set → Allows other scripts to assign/change the value of UIManager
    // The curly braces in { get; set; } define a property, allowing controlled access to the UIManager variable without directly exposing it

    private void Awake() // Private Keeps Awake() internal to GameManager → Other scripts can’t call it directly, ensuring GameManager controls its own initialization
                         // Indicates that the function performs an action but does NOT return anything. Used for functions that execute game logic—like initializing GameManager in Awake()
                         // Awake Runs when the script is first loaded → Before Start(), ensuring GameManager is set up before anything else in the scene
    {
        if (Instance == null) // Is checking whether a GameManager instance already exists before creating a new one
        {
            Instance = this; // Assigns the current GameManager to Instance, making it the official reference for the entire game. Now, other scripts can access GameManager by calling GameManager.Instance instead of searching for it in the scene
            DontDestroyOnLoad(gameObject); // Prevents GameManager from being destroyed when switching scenes → This ensures UI, player stats, and scene logic persist across levels. Without this line, GameManager would be deleted and recreated every time a new scene loads, causing UI bugs and data resets
        }
    }




    // 
    public void OpenScene(int index)
    // public → This means any other script can call this function. void → The function does something but doesn’t return a value. OpenScene(int index) → This function takes one number (index) as input, which tells Unity which scene to load
    {
        SceneManager.LoadScene(index);
        // SceneManager.LoadScene(index); → This switches the game to a different scene, based on the number (index) stored in Build Settings. Example: If index = 2, Unity loads scene #2 from Build Settings
    }


    public void QuitGame()// public → Allows any script to use this function. void → This function does something but doesn’t return data. QuitGame() → A function meant to close the game when called
    {
        Application.Quit(); // Application.Quit(); → Ends the game completely. Important: This only works in a built application (not inside Unity’s editor). Example: If the player clicks a "Quit" button, this function closes the game window!
    }
}

// ===================== GAME MANAGER SUMMARY =====================
// The GameMananager is a singleton
// The GameManager script acts as the central hub for controlling scene transitions, UI management, and game state persistence
// The four singletons in your game—GameManager, PlayerController, and SceneController—cover the core aspects of scene transitions, player control, and scene initialization

// 🔹 Uses the Singleton pattern → Ensures only ONE GameManager exists.
// 🔹 Prevents GameManager from being destroyed across scenes → Keeps player stats & UI consistent
// 🔹 Manages scene switching (`OpenScene(int index)`) → Loads scenes using Unity's SceneManager
// 🔹 Handles quitting the game (`QuitGame()`) → Closes the application when called
// 🔹 Provides a global reference for UIManager → Allows UI updates from any script
//
// Think of GameManager as the **overseer of the entire game**, ensuring smooth scene changes
// UI updates, and game persistence.

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


// ===================== SINGLETON SYSTEM SUMMARY =====================
// Your game has four key singletons:
// GameManager → Handles scene transitions, UI updates, and game state persistence
// PlayerController → Manages FPS mechanics, ensuring only ONE active player exists per scene
// PlayerManager → Stores player stats (health, ammo, lives, and weapons), keeping them persistent across scenes
// SceneController → Oversees scene-specific details like music, player setup, and weapon assignments
//
// The Singleton pattern ensures only ONE instance of each exists at any time
// Prevents duplicate objects from interfering with game mechanics
// Allows global access via GameManager.Instance, PlayerController.Instance, PlayerManager.Instance, and SceneController.Instance
//
// Think of these singletons as the backbone of your game, keeping everything stable 

