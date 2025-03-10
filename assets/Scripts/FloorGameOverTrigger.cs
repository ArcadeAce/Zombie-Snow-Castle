﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class FloorGameOverTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(0);
        }
    }
}