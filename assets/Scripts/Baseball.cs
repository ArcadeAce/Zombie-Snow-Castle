using UnityEngine;

public class Baseball : MonoBehaviour
{
    public int damage = 30;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerManager.Instance.TakeDamage(damage);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject, 5f); // Destroy after 5 seconds if it doesn't hit the player
        }
    }
}

