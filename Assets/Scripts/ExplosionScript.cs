using UnityEngine;

/* This C# script, `ExplosionScript`, is designed to handle the behavior of an explosion in a Unity project. It is
attached to a game object, such as a grenade, and manages the timing, visual effects, and physical impact of the
explosion.

The script begins by defining several public variables that can be adjusted in the Unity editor.

These variables control the delay before the explosion, the force and radius of the explosion, the particle effect to
use, and the amount of damage to apply to affected objects.

The `Update` method continuously decreases the `explosionTime` by the time elapsed since the last frame
(`Time.deltaTime`). When `explosionTime` reaches zero and the explosion has not yet occurred (`!hasExploded`), the
`Explode` method is called, and the `hasExploded` flag is set to true to prevent multiple explosions.

The `Explode` method handles the actual explosion logic. It starts by randomly choosing a start lifetime for the
particle system effect and instantiating the explosion effect at the grenade's position.

Next, the method retrieves all colliders within the explosion radius using `Physics.OverlapSphere` and iterates through
them. For each collider, it applies an explosion force to any rigidbodies found.

The script then checks if the nearby object is an enemy and applies damage if so. It uses a raycast to determine the
hit point and calls the `TakeDamage` method on the enemy.

If the nearby object is the player, it applies damage to the player and logs a message.

Finally, the script destroys the grenade game object after the explosion:

In summary, this script manages the timing, visual effects, and physical impact of an explosion in a Unity game,
applying forces and damage to nearby objects and destroying the grenade object after the explosion.

*/

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


