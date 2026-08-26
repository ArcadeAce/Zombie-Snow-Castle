using System;
using UnityEngine;

public class Baseball : MonoBehaviour
{
    [Header("Damage")]
    public int damage = 30;

    [Header("Curve Settings")]
    public float forwardSpeed = 20f;        // slow speed for testing
    public float curveStrength = 4f;        // sideways bend
    public float curveAcceleration = 1.2f;  // curve increases over time
    public float lifeTime = 7f;

    private Rigidbody rb;
    private float curveDirection;           // +1 = right, -1 = left
    private Transform player;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = PlayerController.Instance.cam.transform;

        // Random left or right curve
        curveDirection = UnityEngine.Random.value > 0.5f ? 1f : -1f;

    }

    void FixedUpdate()
    {
        // Always move toward the player
        Vector3 forwardDir = (player.position - transform.position).normalized;
        rb.velocity = forwardDir * forwardSpeed;

        // Add sideways curve force
        float currentCurve = curveStrength * curveDirection;
        rb.AddForce(transform.right * currentCurve, ForceMode.Acceleration);

        // Curve increases over time (smooth bend)
        curveStrength += curveAcceleration * Time.fixedDeltaTime;

        // Destroy after lifetime
        Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerManager.Instance.TakeDamage(damage);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject, 20f); // Destroy after 20 seconds if it doesn't hit the player
        }
    }
}


