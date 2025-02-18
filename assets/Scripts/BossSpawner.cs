using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    public Enemy ZombieBoss; // Reference to the Enemy prefab
    public string bossMusic; // Boss music ID
    public float distance; // Distance from player to spawn the boss
    private BoxCollider _collider; // Reference to BoxCollider component

    public BossRoomCaveDoorOpen BossRoomCaveDoorOpen; // Reference to BossRoomCaveDoorOpen script

    private void Awake()
    {
        _collider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Transform player = other.transform;
            GameManager.UIManager.DisplayZombiename(ZombieBoss.name);

            // Stop cave sound and play boss music using AudioManager
            AudioManager.Instance.StopMusic("Cave sound");
            AudioManager.Instance.PlayMusic(bossMusic); // Play Poison Zombie boss music

            // Instantiate zombie boss
            Enemy boss = Instantiate(ZombieBoss, player.position + player.forward * -distance, Quaternion.identity);
            boss.Register(this);
            Destroy(_collider);

            // Start coroutine to monitor boss defeat
            StartCoroutine(MonitorBossDefeat(boss));
        }
    }

    private IEnumerator MonitorBossDefeat(Enemy boss)
    {
        // Wait until the boss is defeated
        while (boss != null)
        {
            yield return null;
        }

        // Stop the boss music when the boss is defeated
        AudioManager.Instance.StopMusic(bossMusic);
    }

    internal void OpenDoor()
    {
        BossRoomCaveDoorOpen.OpenDoor();
    }
}


