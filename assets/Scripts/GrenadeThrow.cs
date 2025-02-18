using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeThrow : MonoBehaviour
{
    private GrenadeThrowingZombie enemy;
    public Grenade _Grenade;
    private Vector3 SpawnPos => enemy.grenadeholder.position;

    private void Awake()
    {
        enemy = GetComponentInParent<GrenadeThrowingZombie>();
    }

    public void ThrowGrenade()
    {
        Grenade _grenade=Instantiate(_Grenade,SpawnPos,Quaternion.identity);
        Rigidbody grenadeRb = _grenade.GetComponent<Rigidbody>();

        Vector3 target = GameManager.FPSController.cam.transform.position;
        Vector3 direction = target - SpawnPos;
        Debug.DrawRay(SpawnPos, direction, Color.green);
        float height = direction.y;
        direction.y = 0;
        float distance = direction.magnitude;
        float angle = 45f * Mathf.Deg2Rad;

        direction.y = distance * Mathf.Tan(angle);
        distance += height / Mathf.Tan(angle);

        float velocity = Mathf.Sqrt(distance * Physics.gravity.magnitude / Mathf.Sin(2 * angle));
        grenadeRb.velocity = velocity * direction.normalized;
        Invoke(nameof(ResetThrow), 8f);
        
    }

    private void ResetThrow()
    {
        enemy.readytothrow = true;
    }
}
