using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance;

    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        // 🎵 Play background music for this scene
        AudioManager.Instance.PlayMusic(Constants.CAVE_MUSIC);

        // ✅ Verify PlayerManager exists
        if (PlayerManager.Instance != null)
        {
            ////////////////////////////////////////////Copilot
            // === Re-equip remembered weapon ===
            string rememberedWeapon = PlayerManager.lastHeldWeaponType;
            PlayerManager.Instance.WeaponSwitcher.SwitchWeaponTo(rememberedWeapon);
            UnityEngine.Debug.Log($"[SceneController] Re-equipped {rememberedWeapon}");

            ////////////////////////Copilot



            // ✅ Verify WeaponSwitcher exists
            if (PlayerManager.Instance.WeaponSwitcher != null)
            {
                UnityEngine.Debug.Log("Setting up weapons...");
                PlayerManager.Instance.WeaponSwitcher.SetupWeapons();
                PlayerManager.Instance.WeaponSwitcher.SyncWeaponVisibility();
            }
            else
            {
                UnityEngine.Debug.LogError("WeaponSwitcher is not initialized.");
            }
        }
        else
        {
            UnityEngine.Debug.LogError("PlayerManager.Instance is not initialized.");
        }
    }
}


