using UnityEngine;

public class CreditsFloatUp : MonoBehaviour
{
    public float speed = 0.1f; // Speed of upward movement

    void Update()
    {
        // Move object upward very slowly every frame
        transform.position += Vector3.up * speed * Time.deltaTime;
    }
}
// For credits
