using UnityEngine;

public class SliderZombieThrow : MonoBehaviour
{
    [Header("Baseball Prefabs")]
    public GameObject weakBaseballPrefab;     // Normal baseball
    public GameObject superBaseballPrefab;    // Super baseball

    [Header("Spawn Point")]
    public Transform baseballSpawnPoint;      // Empty on the zombie's hand

    [Header("Pitch Speeds")]
    public float weakPitchSpeed = 20f;        // Weak baseball speed
    public float superPitchSpeed = 40f;       // Super baseball speed

    [Header("Attack Settings")]
    public bool useSuperPitch = false;        // True = super pitch

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Called by animation event at the exact throw frame
    public void SpawnBaseball()
    {
        // Pick correct baseball prefab
        GameObject prefabToThrow = useSuperPitch ? superBaseballPrefab : weakBaseballPrefab;

        // Spawn baseball at the hand
        GameObject baseball = Instantiate(prefabToThrow, baseballSpawnPoint.position, baseballSpawnPoint.rotation);

        // Move baseball toward the player
        Rigidbody rb = baseball.GetComponent<Rigidbody>();

        // Direction from zombie hand to player
        Vector3 direction = (PlayerController.Instance.cam.transform.position - baseballSpawnPoint.position).normalized;

        // Pick correct speed
        float speed = useSuperPitch ? superPitchSpeed : weakPitchSpeed;

        // Launch baseball
        rb.velocity = direction * speed;
    }
}

