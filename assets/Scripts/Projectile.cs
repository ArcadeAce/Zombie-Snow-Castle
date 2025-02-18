using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float throwForce = 20f;
    public float StoppingThreshold = 0.1f;
    public float hitThreshold = 1f;

    private Rigidbody rb;
    private bool hasHitGround = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * throwForce, ForceMode.Impulse);

    }

    private void FixedUpdate()
    {
        if (rb.velocity.magnitude < StoppingThreshold && !hasHitGround)
        {
            rb.velocity = Vector3.zero;
            rb.isKinematic = true;
            hasHitGround = true;
            Debug.Log("Object has hit ground");
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude >= hitThreshold && !hasHitGround)
        {
            hasHitGround = true;
            Debug.Log("Object has hit the ground.");

        }

    }
}
    
