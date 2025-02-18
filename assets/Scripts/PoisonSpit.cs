using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//1. Poison spit happens when animation event happens//
public class PoisonSpit : MonoBehaviour
{

    private PoisonZombie enemy;

    public Poisonspitcloud _poisonspit;
    public float force;

    private Vector3 spawnpos => enemy.Poisonspitholder.position;
    private void Awake()
    {
        enemy = GetComponentInParent<PoisonZombie>();
    }
    public void SpitPoison()
    {
        Poisonspitcloud poisonspit = Instantiate(_poisonspit, spawnpos, Quaternion.identity);
        Rigidbody poisonspitcloud = poisonspit.GetComponent<Rigidbody>();
        //2. Poison spit then travels after being created, then travels in a straight line to where the zombie was looking at//
        poisonspitcloud.AddForce(transform.forward * force,ForceMode.Impulse);

        Invoke(nameof(ResetSpit), 10f);
    }

    private void ResetSpit()
    {
        enemy.readytospit = true;
       
    }


  










    //3. Poison spit if hits the wall stays at the wall if possible, and continues with its lifetime animation till it dies//


}
