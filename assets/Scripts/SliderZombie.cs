using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderZombie : Enemy
{
    public GameObject baseballPrefab; // Reference to the baseball prefab
    public Transform throwPoint; // Reference to the empty gameobject for baseball to come out of
    public float throwForce = 10f;
    public float throwCooldown = 3f;
    private bool canThrow = true;

    public override void Awake()
    {
        base.Awake();
    }

    public override void Update()
    {
        base.Update();
        if (playerInRange && canThrow)
        {
            TryThrowBaseball();
        }
    }

    private void TryThrowBaseball()
    {
        canThrow = false;
        Animator.SetTrigger("Throw");
        Invoke("ThrowBaseball", 1f); // Adjust timing based on the animation
        Invoke("ResetThrow", throwCooldown);
    }

    private void ThrowBaseball()
    {
        GameObject baseball = Instantiate(baseballPrefab, throwPoint.position, throwPoint.rotation);
        Rigidbody rb = baseball.GetComponent<Rigidbody>();
        rb.velocity = throwPoint.forward * throwForce;
    }

    private void ResetThrow()
    {
        canThrow = true;
    }
}
