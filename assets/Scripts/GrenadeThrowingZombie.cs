using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeThrowingZombie : Enemy
{
    public Transform grenadeholder;
    public float throwdistance;
    private bool inrange => playerInRange && Vector3.Distance(transform.position, PlayerManager.Instance.transform.position) >= throwdistance;
    public bool readytothrow
    {
        get; set;
    }
    public override void Awake()
    {
        base.Awake();
        readytothrow = true;
    }

    public override void Update()
    {
        base.Update();
        if(readytothrow && inrange)
        {
            Animator.SetTrigger("Throw");
            readytothrow = false;
        }
    }
}
