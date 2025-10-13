using System;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    private Entity adjacentEntity;
    protected override void Update()
    {
        if (player.gameObject.activeSelf && Vector3.Distance(player.transform.position, transform.position) <
            Vector3.Distance(centralBuilding.transform.position, transform.position))
        {
            target = player.transform;
        }
        else
        {
            target = centralBuilding.transform;
        }
        HandleAttack();
    }

    protected override void HandleAttack()
    {
        fireRateTimer += Time.deltaTime;
        if (fireRateTimer > stats.fireRate && adjacentEntity)
        {
            adjacentEntity.TakeDamage(stats.damage);
            fireRateTimer = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(tag))
        {
            if (other.gameObject.TryGetComponent(out Entity isEntity))
            {
                adjacentEntity = isEntity;
            }
        }
    }
}
