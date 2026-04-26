using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickUp : MonoBehaviour
{
    
    public Weapon Prefab; // The Twin Turbos weapon prefab that will be picked up. Make sure to assign the correct prefab in the Unity Inspector for this script to work properly.

    public GameObject fakeHandgun; // A visual representation of the Twin Turbos prop weapon to dissapear when picked up. Make sure to assign the correct Twin Turbos prop prefab GameObject in the Unity Inspector for this script to work properly. This should be a child of the Chest Wooden gameobject and should be the visual representation of the Twin Turbos in the chest. It will be destroyed when the player picks up the weapon, giving the illusion that the player took the weapon from the chest.

    private Animator Animator; // Reference to the Animator component, private because nothing outside this script should ever control the chest’s animation.

    private bool isOpen; // To make sure chest does not open multiple times when player enters the trigger zone multiple times

    private void Awake()
    {
      
        Animator = GetComponent<Animator>();  // Get the Animator component attached to this object
    }

    void OnTriggerEnter(Collider other) // when player enters the box collider for the wooden chest to open
    {
        
        if (other.CompareTag("Player") && !isOpen) // Check if the player enters the trigger zone and the interaction hasn't happened yet
        {
         
            isOpen = true; // Set the interaction as open to prevent multiple interactions

           
            StartCoroutine(Open()); // Start the Open coroutine
        }
    }

    IEnumerator Open()
    {
   
        Animator.SetTrigger("Open"); // Trigger an "Open" animation

     
        yield return new WaitForSeconds(3f); // It delays the weapon pickup until the wooden chest animation is done

        PlayerManager.Instance.WeaponSwitcher.Pickup(Prefab,true); // Pick up the weapon and add it to the player's inventory or equip it

        
        Destroy(fakeHandgun); // Destroys the visual representation of the Twin turbos in the wooden chest. The Twin Turbos pistols need to be a child of the Weapons gameobect, that is a child of the Chest Wooden.

        
        AudioManager.Instance.PlayEffect("Weapon Pick Up"); // Play an audio effect for Twin Turbos being picked up. Make sure to have an audio clip named "Weapon Pick Up" in your AudioManager.
    }
}

