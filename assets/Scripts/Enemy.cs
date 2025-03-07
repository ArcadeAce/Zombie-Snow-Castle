using System;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    // Particle systems for death explosion and hit effect
    public ParticleSystem deathExplosion;
    public ParticleSystem hitEffect;

    // Reference to the enemy's rigid body
    public Rigidbody rigidBody;

    // Animator for enemy animations
    protected Animator Animator;

    // NavMesh agent for enemy movement
    protected NavMeshAgent agent;

    // Reference to the boss spawner (if this enemy is a boss)
    private BossSpawner spawner;

    // Current health of the enemy
    public int health;


    // Damage inflicted by the enemy
    public int damage;

    // Distance at which the enemy starts chasing the player
    public float chaseDistance;



    // Delay before actions (not currently used)
    public float delay;

 

    // Flag to prevent multiple hit effects
    private bool particlePlaying;



    // Flag to check if this enemy is a boss
    private bool Boss;

    // Property to check if the player is in the enemy's chase range
    protected bool playerInRange => Vector3.Distance(transform.position, PlayerController.Instance.cam.transform.position) <= chaseDistance;

    // Property to check if the enemy is currently chasing the player
    public bool IsChasing { get; private set; }



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

    internal void Patrol(Vector3 position)
    {
        // Set the destination for patrolling
        agent.SetDestination(position);
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
        PlayerManager.Instance.TakeDamage(damage);
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
            //TODO in the Audio Manager in element 7, in Audio Manager, This grenade explosion sound is temporaray and replace with better sound later, and rewrite the name in the in the quotes below, use AudioManager.Instance.PlayEffect("Grenade explosion, pixabay, hq-explosion-6288"); for adding all sounds.
            AudioManager.Instance.PlayEffect("Grenade explosion new");
            AudioManager.Instance.PlayEffect("Boss death music");


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











