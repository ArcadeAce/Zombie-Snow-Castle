using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    public float explosionTime = 3f; // Time before explosion
    public float explosionForce = 10f; // Force of the explosion
    public float explosionRadius = 5f; // Radius of the explosion
    public ParticleSystem explosionEffect; // Particle system for the explosion
    public int damageAmount = 50; // Amount of damage to apply

    private bool hasExploded = false; // Flag to check if the object has already exploded

    void Update()
    {
        explosionTime -= Time.deltaTime;

        if (explosionTime <= 0f && !hasExploded)
        {
            Explode();
            hasExploded = true;
        }
    }

    void Explode()
    {
        // Randomly choose between two start lifetimes (1.7 and 10.0 seconds)
        float randomStartLifetime = Random.Range(0, 2) == 0 ? 1.7f : 10.0f;

        // Instantiate the explosion effect at the grenade's position
        ParticleSystem instantiatedExplosion = Instantiate(explosionEffect, transform.position, Quaternion.identity);

        // Modify the particle system's start lifetime
        var mainModule = instantiatedExplosion.main;
        mainModule.startLifetime = randomStartLifetime;

        // Get all colliders within the explosion radius
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider nearbyObject in colliders)
        {
            // Apply explosion force to all rigidbodies within the radius
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            }

            // Check if the nearby object is an Enemy and apply damage
            Enemy enemy = nearbyObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                RaycastHit hit;
                Physics.Raycast(transform.position, nearbyObject.transform.position - transform.position, out hit);
                enemy.TakeDamage(damageAmount, hit); // Apply damage to the enemy
            }

            // Check if the nearby object is the player
            if (nearbyObject.CompareTag("Player"))
            {
                // Apply damage to the player
                PlayerManager.Instance.TakeDamage(damageAmount); // Apply damage to the player
                Debug.Log("Player took damage from grenade explosion.");
            }
        }

        // Destroy the grenade after the explosion
        Destroy(gameObject);
    }
}


