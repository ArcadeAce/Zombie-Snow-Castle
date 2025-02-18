using UnityEngine;

public class BaseballDamage : MonoBehaviour
{
    public int damage = 20;  // Damage to be applied to the player on collision

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Fix this later so it does not affect other scripts so you can move in your game, and no disasters//
            // Assuming the player has a health script
            //other.GetComponent<PlayerHealth>().TakeDamage(damage);//

            // Destroy the baseball after it hits the player
            Destroy(gameObject);
        }
    }
}

