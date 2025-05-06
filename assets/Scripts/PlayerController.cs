using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    public Camera cam;
    public Transform WeaponHolder;
    public static PlayerController Instance
    {
        get;
        private set;
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
            DontDestroyOnLoad(gameObject);
        }
    }
}


