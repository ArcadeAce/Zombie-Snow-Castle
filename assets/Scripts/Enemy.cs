using System;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public int health; // Current health of the enemy
    public int zombieBossDamage; // Damage inflicted by the enemy
    public ParticleSystem hitEffect; // For the particle system to go off when the zombie boss is hit when shot
    public ParticleSystem deathExplosion; // Particle systems for death explosion when the zombie boss dies.
    public Rigidbody rigidBody; // Reference to the enemy's rigid body
    public float zombieBossesChasingDistanceToFollowThePlayer; // Distance at which the enemy chases the player
    public float delay; // Delay before actions used?

    protected Animator Animator; // Animator for enemy animations
    protected NavMeshAgent agent; // NavMesh agent for enemy movement
 
    private BossSpawner spawner; // Reference to the boss spawner (if this enemy is a boss)
    private bool particlePlaying; // Flag to prevent multiple hit effects
    private bool Boss;// Flag to check if this enemy is a boss







    // Property to check if the player is in the enemy's chase range
    protected bool playerInRange => Vector3.Distance(transform.position, PlayerController.Instance.cam.transform.position) <= zombieBossesChasingDistanceToFollowThePlayer;

    
    public bool IsChasing { get; private set; }// Property to check if the enemy is currently chasing the player







    public virtual void Awake()
    {
        // Get the NavMesh agent component
        agent = GetComponent<NavMeshAgent>();

        // Try to get the Animator component (may be on this object or its children)
        if (TryGetComponent<Animator>(out Animator animator))
        {
            Animator = animator;
        }
        else
        {
            Animator = GetComponentInChildren<Animator>();
        }
    }

    public void Register(BossSpawner bossSpawner)
    {
        // Register this enemy as a boss and store a reference to the boss spawner
        Boss = true;
        spawner = bossSpawner;
    }


    public virtual void Update()
    {
        if (playerInRange)
        {
            // If the player is in range, set the destination to the player's position
            agent.SetDestination(PlayerController.Instance.cam.transform.position);
            IsChasing = true;
        }

        if (!playerInRange)
        {
            // If the player is out of range, stop chasing
            IsChasing = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Trigger the attack animation when the player enters the trigger zone
            Animator.SetTrigger("Attack");
        }
    }







    public void DealDamage()
    {
        // Deal damage to the player
        PlayerManager.Instance.TakeDamage(zombieBossDamage);
    }

    public void TakeDamage(int amount, RaycastHit hit)
    {
        // Reduce the enemy's health by the given amount
        health -= amount;

        if (health <= 0f)
        {
            // If the enemy's health reaches zero or below, trigger death
            Die();
        }

        if (!particlePlaying)
        {
            // Play hit effects and apply force to the enemy
            ShowEffects(hit);
            particlePlaying = true;
        }
        else
        {
            particlePlaying = false;
        }
    }







    private void ShowEffects(RaycastHit hit)
    {
        // Position the hit effect at the hit point and play it
        hitEffect.gameObject.transform.position = hit.point;
        hitEffect.Play();


    }






    private void Die()
    {
        // Detach the death explosion, play it, and destroy the enemy

        deathExplosion.transform.parent = null;
        deathExplosion.Play();

        if (Boss)
        {
            // If this enemy is a boss, handle boss-related actions
            GameManager.UIManager.TurnNameOff();
            AudioManager.Instance.StopMusic("Boss music");

            //use AudioManager.Instance.PlayEffect("example"); for adding all sounds.
            
            AudioManager.Instance.PlayEffect("Boss death music");
            AudioManager.Instance.PlayEffect("Grenade explosion sound and zombie exploding sound");

            spawner.OpenDoor();
            Invoke("PlaySceneMusic", 10f);
        }
        else
        {

        }

        Destroy(gameObject);
    }







    private void PlaySceneMusic()
    {
        // Play the scene music (e.g., after the boss fight)
        AudioManager.Instance.PlayMusic("Cave music");
    }
}
// ===================== ENEMY SCRIPT SUMMARY =====================
// 🔹 The Enemy script controls **zombie AI behavior**, including movement, attacking, and death mechanics.
//
// ✅ Core Responsibilities:
// ✅ Uses **NavMeshAgent** for intelligent navigation and chasing the player.
// ✅ Tracks health (`health`) → Manages enemy damage and death conditions.
// ✅ Handles **player detection & chasing** → Moves toward the player when in range.
// ✅ Manages attack animation (`Animator.SetTrigger("Attack")`) when the player enters the trigger zone.
// ✅ Plays **hit effects (`ShowEffects()`)** when the enemy gets damaged.
// ✅ Handles death behavior (`Die()`) → Plays explosion effects and destroys the enemy object.
// ✅ Special logic for **boss zombies** → Stops boss music, triggers explosions, and opens doors upon defeat.
//
// ❌ What It Does NOT Do:
// ❌ Does NOT spawn enemies → Spawning is handled separately by **BossSpawner**.
// ❌ Does NOT control player health → It only **deals damage** to the player, but the actual health system is managed by **PlayerManager**.
//
// 🔹 Think of the Enemy script as **the AI system controlling zombies**, ensuring they chase, attack, take damage, and trigger special events when defeated!











