using UnityEngine;

public class CurveballZombieThrow : MonoBehaviour
{
    [Header("Curveball Prefabs")]
    public GameObject weakCurveballPrefab;
    public GameObject superCurveballPrefab;

    [Header("Spawn Points")]
    public Transform weakSpawnPoint;
    public Transform superSpawnPoint;

    [Header("Pitch Speeds")]
    public float weakPitchSpeed = 57f;
    public float superPitchSpeed = 57f;

    [Header("Attack Settings")]
    public bool useSuperPitch = false;

    // Animation event
    public void SpawnCurveball()
    {
        GameObject prefabToThrow = useSuperPitch ? superCurveballPrefab : weakCurveballPrefab;
        Transform spawnPoint = useSuperPitch ? superSpawnPoint : weakSpawnPoint;

        GameObject curveball = Instantiate(prefabToThrow, spawnPoint.position, spawnPoint.rotation);
        Rigidbody rb = curveball.GetComponent<Rigidbody>();

        Vector3 direction = (PlayerController.Instance.cam.transform.position - spawnPoint.position).normalized;
        float speed = useSuperPitch ? superPitchSpeed : weakPitchSpeed;

        rb.velocity = direction * speed;

        Destroy(curveball, 7f);
    }
}

