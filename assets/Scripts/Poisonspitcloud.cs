using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poisonspitcloud : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
       if (other.CompareTag("Player"))
        {
            PlayerManager.Instance.TakeDamage(40);
            Destroy(gameObject);
        }
    }
}
