using UnityEngine;

public class SliderZombiesBaseball : MonoBehaviour
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
            Destroy(gameObject, 7f); // Destroy the baseball after 7 seconds if it doesn't hit the player
        }
    }
}
