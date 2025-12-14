using System.Collections; // Provides basic collection tools (like Arrays, Lists).
using System.Collections.Generic; // Expands collection capabilities (e.g., Lists<>).
using UnityEngine; // Grants access to Unity's core functionality (GameObjects, physics, UI).

public class GameAssets : MonoBehaviour // 🎮 Defines a GameAssets class that stores essential game objects like weapons.
{
    public static GameAssets Instance; // 🔄 Ensures a single **GameAssets instance exists** across scenes (Singleton pattern).
    public Weapon[] Weaponprefabs; // 🔫 Stores **all weapon prefabs** → Allows the game to spawn weapons dynamically.

    private void Awake() // Runs **automatically when the scene loads** → Initializes the GameAssets instance.
    {
        if (Instance == null) // ❓ Checks if **GameAssets already exists** to prevent multiple instances.
        {
            Instance = this; // Assigns this GameAssets instance as the **global reference** for weapon storage.
            DontDestroyOnLoad(this); // 🔄 Ensures **GameAssets persists across scene transitions** → Prevents deletion when switching levels.
        }
    }
}
// // ===================== GAME ASSETS SUMMARY =====================
// 🔹 The GameAssets script serves as a **centralized storage manager** for essential objects like weapons.
// 🔹 Uses Singleton Pattern (`public static GameAssets Instance`) → Ensures **only ONE GameAssets object exists globally**.
// 🔹 Stores weapon prefabs (`public Weapon[] Weaponprefabs`) → Provides **easy access to weapon models for spawning**.
// 🔹 Uses `DontDestroyOnLoad(this)` → Prevents deletion when switching scenes, keeping weapon assets available throughout the game.
// 🔹 Helps other scripts quickly retrieve weapons (`Weaponprefabs`) → Improves **modularity and accessibility** in weapon management.
//
// ✅ Think of GameAssets as **your game’s inventory system**, ensuring all weapons stay organized and available!


