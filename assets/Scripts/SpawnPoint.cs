using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class SpawnPoint : MonoBehaviour
{
    GameObject player;
    CharacterController characterController;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Assert.IsNotNull(player, "could not find player");
        characterController = player.GetComponentInChildren<CharacterController>();
        Assert.IsNotNull(characterController, "could not find CharacterController");

        characterController.enabled = false;
        Debug.Log($"moved from position {player.transform.position} to {transform.position}");
        characterController.gameObject.transform.position = transform.position;
        characterController.gameObject.transform.rotation = transform.rotation;
        characterController.enabled = true;
        Debug.Break();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
