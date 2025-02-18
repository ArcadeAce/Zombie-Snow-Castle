using UnityEngine;
using System.Collections;

public class ExplodeObject : MonoBehaviour
{
    public GameObject explosionPrefab;

    void Start()
    {
        Invoke("Explode", 5f);
    }

    void Explode()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}

