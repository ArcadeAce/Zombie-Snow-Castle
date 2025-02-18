using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour
{
    // Reference to the parent Enemy script.
    private Enemy enemy;

    // Called when the script starts.
    void Start()
    {
        // Find and assign the parent GameObject's Enemy component.
        enemy = GetComponentInParent<Enemy>();
    }

    // This method is called from an animation event.
    // It triggers the damage-dealing logic in the Enemy script.
    public void DealDamage()
    {
        // Call the DealDamage method in the Enemy script.
        // This method typically handles damage calculations and applying damage to the player or target.
        enemy.DealDamage();
    }
}

