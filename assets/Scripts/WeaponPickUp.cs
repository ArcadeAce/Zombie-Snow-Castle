using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickUp : MonoBehaviour
{
    // The weapon prefab that will be picked up
    public Weapon Prefab;

    // A visual representation of the weapon to replace when picked up
    public GameObject fakeHandgun;

    // Reference to the Animator component
    private Animator Animator;

    // Flag to ensure interaction only happens once
    private bool isOpen;

    private void Awake()
    {
        // Get the Animator component attached to this object
        Animator = GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the player enters the trigger zone and the interaction hasn't happened yet
        if (other.CompareTag("Player") && !isOpen)
        {
            // Set the interaction as open to prevent multiple interactions
            isOpen = true;

            // Start the Open coroutine
            StartCoroutine(Open());
        }
    }

    IEnumerator Open()
    {
        // Trigger an "Open" animation
        Animator.SetTrigger("Open");

        // Wait for 3 seconds before executing the following actions
        yield return new WaitForSeconds(3f);

        // Pick up the weapon and add it to the player's inventory or equip it
        PlayerManager.Instance.WeaponSwitcher.Pickup(Prefab,true);

        // Destroy the visual representation of the weapon
        Destroy(fakeHandgun);

        // Play an audio effect for weapon pickup
        AudioManager.Instance.PlayEffect("Weapon Pick Up");
    }
}

