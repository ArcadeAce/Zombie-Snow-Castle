using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoomCaveDoorOpen : MonoBehaviour
{
    private Animator Animator;
    void Start()
    {
        Animator = GetComponent<Animator>();
    }

    public void OpenDoor()
    {
        Animator.SetTrigger("open");
    }
}
