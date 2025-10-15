using System;
using TMPro;
using UnityEngine;

public class CentralBuilding : Entity
{
    public PlayerController player;
    public float respawnTimer;
    public float endOfWaveHealing;
    public float endOfRoundHealing;
    public Transform healthBar;
    public TextMeshProUGUI healthBarText;

    private void Start()
    {
        UpdateHealthBar();
    }

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
                player.UpdateHealthBar();
            }
        }
    }
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        UpdateHealthBar();
    }
    public override void Heal(float healing)
    {
        base.Heal(healing);
        UpdateHealthBar();
    }
    public void UpdateHealthBar()
    {
        healthBar.localScale = new Vector2(health / maxHealth, 1);
        healthBarText.text = $"{health}/{maxHealth}"; //neat!
    }
}
