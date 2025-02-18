using UnityEngine;

public class MirageZombiesShurikenSpin : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float spinSpeed = 90f;

    void Update()
    {
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        transform.Rotate(Vector3.left * spinSpeed * Time.deltaTime);
    }
}
