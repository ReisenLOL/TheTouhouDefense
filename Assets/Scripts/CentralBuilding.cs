using System;
using UnityEngine;

public class CentralBuilding : Entity
{
    public PlayerController player;
    public float respawnTimer;

    private void Update()
    {
        if (player.isRespawning)
        {
            respawnTimer += Time.deltaTime;
            if (respawnTimer > player.respawnDelay)
            {
                player.gameObject.SetActive(true);
                player.isRespawning = false;
                respawnTimer = 0;
                player.transform.position = transform.position;
                player.health = player.maxHealth;
            }
        }
    }
}
