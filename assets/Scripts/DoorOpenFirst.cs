using UnityEngine;

public class DoorOpenFirst : MonoBehaviour
{
    public AudioSource doorSound;
    [SerializeField] private Animator animator;
    bool doorClosed = false;

    void Start()
    {
        doorSound = GetComponent<AudioSource>();
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!doorClosed)
            {
                animator.SetTrigger("CloseDoor");
                doorSound.PlayDelayed(2.0f);
                doorClosed = true;
            }
        }

    }
}