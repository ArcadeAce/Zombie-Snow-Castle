
using UnityEngine;

public class ParticleSystemMovement : MonoBehaviour
{
    public float speed = 100f;
    private ParticleSystem ParticleSystem;

    private void Start()
    {
        ParticleSystem system = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
        ParticleSystem.VelocityOverLifetimeModule velocity = ParticleSystem.velocityOverLifetime;
        velocity.y = speed;
    }

}

