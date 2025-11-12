using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static UIManager UIManager { get; set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void OpenScene(int index)
    {
        if (PlayerManager.Instance != null)
            PlayerManager.lastHeldWeaponType = PlayerManager.Instance.selectedWeaponType;

        SceneManager.LoadScene(index);
    }

    public void QuitGame()
    {
        Application.Quit();
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

