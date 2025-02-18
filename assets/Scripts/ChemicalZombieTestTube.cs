
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChemicalZombieTestTube : MonoBehaviour
{
    public GameObject explosion;
    private SphereCollider hitcollider;
    public int damage;
    public float time;

    private void Start()
    {
        hitcollider = GetComponent<SphereCollider>();
        StartCoroutine(Detonate());
    }

    private IEnumerator Detonate()
    {
        yield return new WaitForSeconds(time);

        GameObject effect = Instantiate(explosion, transform.position, Quaternion.identity);
        AudioManager.Instance.PlayEffect(Constants.GRENADE_EXPLOSION);
        hitcollider.enabled = true;
        transform.GetChild(0).gameObject.SetActive(false);
        yield return new WaitForSeconds(10f);
        Destroy(effect);
        Destroy(gameObject);

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerManager.Instance.TakeDamage(damage);
        }
    }
}
