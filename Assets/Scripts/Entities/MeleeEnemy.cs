using System;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    private Entity adjacentEntity;
    private float attackTime;
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
    }

    protected override void HandleAttack()
    {
        if (Time.time >= attackTime && adjacentEntity)
        {
            attackTime = Time.time + stats.fireRate; //interesting!
            adjacentEntity.TakeDamage(stats.damage);
            fireRateTimer = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag(tag))
        {
            if (other.gameObject.TryGetComponent(out Entity isEntity))
            {
                adjacentEntity = isEntity;
                HandleAttack();
            }
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag(tag))
        {
            if (other.gameObject.TryGetComponent(out Entity isEntity))
            {
                adjacentEntity = isEntity;
                HandleAttack();
            }
        }
    }
}
