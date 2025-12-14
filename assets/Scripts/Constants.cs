using System.Collections; // Provides basic collection tools like Arrays and Lists.
using System.Collections.Generic; // Expands collection capabilities with Lists<> and Dictionaries.
using UnityEngine; // Grants access to Unity’s core functionality like GameObjects and Scene Management.

public static class Constants // 🔹 Defines **Constants**, a **static class** that stores fixed values used throughout the game.
{
    public const string CAVE_MUSIC = "Cave sound"; // 🎵 Stores the **audio ID for cave background music** → Used by `AudioManager` for playback.
    public const string GRENADE_EXPLOSION = "Grenade explosion 4"; // 💥 Stores the **audio ID for grenade explosion sound effect** → Used by `AudioManager` to trigger explosions.
}

// ===================== CONSTANTS SCRIPT SUMMARY =====================
// 🔹 The Constants script serves as a **global storage hub** for frequently used values, ensuring consistency across scripts.
//
// ✅ Core Responsibilities:
// ✅ Uses `public static class Constants` → Allows easy **access to predefined values** from anywhere in the game.
// ✅ Stores **audio track IDs** (`CAVE_MUSIC`, `GRENADE_EXPLOSION`) → Prevents hardcoding and improves organization.
// ✅ Works directly with **AudioManager** → Allows sound effects and music to be referenced dynamically instead of manually typed.
//
// ❌ What It Does NOT Do:
// ❌ Does **not** play sounds itself → Only provides **stored values** for reference.
// ❌ Does **not** handle game logic or scene management → Exists purely for defining reusable data.
//
// 🔹 Think of Constants as **a game-wide reference book**, storing key values to keep your code clean, efficient, and organized!



