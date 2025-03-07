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
        AudioManager.Instance.PlayMusic(Constants.CAVE_MUSIC);

        // Ensure PlayerManager and WeaponSwitcher are initialized before calling SetupWeapons
        if (PlayerManager.Instance != null)
        {
            if (PlayerManager.Instance.WeaponSwitcher != null)
            {
                Debug.Log("Setting up weapons...");
                PlayerManager.Instance.WeaponSwitcher.SetupWeapons();
            }
            else
            {
                Debug.LogError("WeaponSwitcher is not initialized.");
            }
        }
        else
        {
            Debug.LogError("PlayerManager.Instance is not initialized.");
        }
    }
}


