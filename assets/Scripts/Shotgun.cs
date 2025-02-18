using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Weapon
{
    public GameObject barrel;
    private ShootSound shootsound;
    private Quaternion rotation;
    private bool instantiated;
    private float ZRotation;
    
    public override void Start()
    {
        base.Start();
        shootsound = GetComponent<ShootSound>();
        ZRotation = -90f;
        rotation = Quaternion.Euler(0f, -90f, ZRotation);
        barrel.transform.localRotation = rotation;
        instantiated = true;
    }

    public void SpinBarrel()
    {
        Debug.Log("spin");
        StartCoroutine(Spin());
    }

    private IEnumerator Spin()
    {
        yield return new WaitForSeconds(0.5f);
        _animator.SetTrigger("Charge Up");
        yield return new WaitForSeconds(0.1f);
        shootsound.PlaySpinSound();
        ZRotation += 72f;
        rotation = Quaternion.Euler(0f, -90f, ZRotation);

    }

    public override void Shoot()
    {
        Debug.Log("Firing the shotgun");
        // Decrease bullets in the clip
        PlayerManager.Instance.shotgunShells--;

        // Ensure non-negative number of bullets in the clip
        if (PlayerManager.Instance.shotgunShells < 0)
        {
            PlayerManager.Instance.shotgunShells = 0;
        }

        // Update UI to display bullets in the clip
        GameManager.UIManager.UpdateShotgunShells(PlayerManager.Instance.shotgunShells);

        // Perform a Raycast to detect enemies
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.position, fpsCam.forward, out hit, 1000f))
        {
            // Check if the hit object has an Enemy component
            if (hit.collider && hit.collider.transform.TryGetComponent(out Enemy enemy))
            {
                // Deal damage to the enemy
                enemy.TakeDamage(damage, hit);
            }
            // Check if the hit object's parent has an Enemy component
            else if (hit.collider.transform.parent != null)
            {
                if (hit.collider.transform.parent.TryGetComponent(out Enemy enemy2))
                {
                    // Deal damage to the enemy's parent
                    enemy2.TakeDamage(damage, hit);
                }
            }
        }

    }


private void FixedUpdate()
    {
        if (!instantiated)
        {
            return;
        }

        barrel.transform.localRotation = Quaternion.Slerp(barrel.transform.localRotation, rotation, Time.deltaTime * 5f);
    }
}