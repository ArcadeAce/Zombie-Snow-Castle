using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Camera cam;
    public Transform WeaponHolder; 
    private void OnEnable()
    {
        GameManager.FPSController = this;

    }
}

