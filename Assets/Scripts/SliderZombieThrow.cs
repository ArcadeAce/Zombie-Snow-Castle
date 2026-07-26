using UnityEngine;

public class SliderZombieThrow : MonoBehaviour
{
    [Header("Baseball Prefabs")]
    public GameObject weakBaseballPrefab;     // The normal baseball the zombie throws
    public GameObject superBaseballPrefab;    // The flame baseball for the super pitch

    [Header("Spawn Point")]
    public Transform baseballSpawnPoint;      // Empty object on the zombie's hand where the baseball appears

    [Header("Pitch Speeds")]
    public float weakPitchSpeed = 20f;        // Speed of the weak baseball
    public float superPitchSpeed = 40f;       // Speed of the super baseball

    [Header("Attack Settings")]
    public bool useSuperPitch = false;        // True = super pitch, False = weak pitch

    private Animator animator;                // Reference to the Animator on the zombie

    private void Start()
    {
        // Get the Animator from the same GameObject this script is attached to
        animator = GetComponent<Animator>();
    }

    // This function is called by the animation event at the exact release frame
    public void SpawnBaseball()
    {
        // Choose which baseball to throw based on the attack type
        GameObject prefabToThrow = useSuperPitch ? superBaseballPrefab : weakBaseballPrefab;

        // Create the baseball at the hand's spawn point
        GameObject baseball = Instantiate(prefabToThrow, baseballSpawnPoint.position, baseballSpawnPoint.rotation);

        // Get the Rigidbody so we can make it move
        Rigidbody rb = baseball.GetComponent<Rigidbody>();

        // The forward direction of the hand (where the baseball should fly)
        Vector3 direction = baseballSpawnPoint.forward;

        // Pick the correct speed depending on weak or super pitch
        float speed = useSuperPitch ? superPitchSpeed : weakPitchSpeed;

        // Make the baseball fly forward using velocity
        rb.velocity = direction * speed;
    }
}

