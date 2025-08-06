using System;
using System.Collections;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public GameObject explosion;         // Explosion particle effect prefab
    public int damage = 30;              // Damage dealt to the player
    public float time = 4.75f;           // Fuse time before explosion

    private SphereCollider hitcollider;  // Reference to the damage collider

    private void Start()
    {
        hitcollider = GetComponent<SphereCollider>();
        hitcollider.enabled = false; // Start disabled
        StartCoroutine(Detonate());
    }

    private IEnumerator Detonate()
    {
        yield return new WaitForSeconds(time); // Wait for fuse

        // Spawn explosion effect
        GameObject effect = Instantiate(explosion, transform.position, Quaternion.identity);
        AudioManager.Instance.PlayEffect(Constants.GRENADE_EXPLOSION);

        // Enable damage zone briefly
        hitcollider.enabled = true;

        // Hide grenade mesh (assumes it's the first child)
        if (transform.childCount > 0)
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }

        yield return new WaitForSeconds(0.5f); // Damage window

        // Disable damage zone
        hitcollider.enabled = false;

        // Stop physics movement
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true;
        }

        // Clean up explosion effect and grenade object
        Destroy(effect, 1f);
        Destroy(gameObject, 1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hitcollider.enabled && other.CompareTag("Player"))
        {
            PlayerManager.Instance.TakeDamage(damage);
        }
    }
}

