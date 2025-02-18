using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonZombie : Enemy
{
    public Transform Poisonspitholder;

    public float spitdistance;

    private bool inrange => playerInRange && Vector3.Distance(transform.position, PlayerManager.Instance.transform.position) >= spitdistance;

    public bool readytospit
    {
        get; set;

    }
    public override void Awake()
    {
        base.Awake();
        readytospit = true;
    }

    public override void Update()
    {
        base.Update();
        if(readytospit && inrange)
        {
            Animator.SetTrigger("Poisonspit");
            readytospit = false;
        }
    }
}
