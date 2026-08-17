using UnityEngine;

public class SliderZombieThrow : MonoBehaviour
{
    [Header("Baseball Prefabs")]
    public GameObject weakBaseballPrefab;
    public GameObject superBaseballPrefab;

    [Header("Spawn Points")]
    public Transform weakSpawnPoint;
    public Transform superSpawnPoint;

    [Header("Pitch Speeds")]
    public float weakPitchSpeed = 20f;
    public float superPitchSpeed = 40f;

    [Header("Attack Settings")]
    public bool useSuperPitch = false;

    // Animation event
    public void SpawnBaseball()
    {
        GameObject prefabToThrow = useSuperPitch ? superBaseballPrefab : weakBaseballPrefab;
        Transform spawnPoint = useSuperPitch ? superSpawnPoint : weakSpawnPoint;

        GameObject baseball = Instantiate(prefabToThrow, spawnPoint.position, spawnPoint.rotation);
        Rigidbody rb = baseball.GetComponent<Rigidbody>();
        Debug.Break();

        Vector3 direction = (PlayerController.Instance.cam.transform.position - spawnPoint.position).normalized;
        float speed = useSuperPitch ? superPitchSpeed : weakPitchSpeed;

        rb.velocity = direction * speed;

        Destroy(baseball, 20f);
    }
}



