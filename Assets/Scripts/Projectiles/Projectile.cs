using System;
using Core.Extensions;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float damage;
    public float timeUntilAutoDestroy;

    public void RotateToTarget(Vector3 direction)
    {
        transform.Lookat2D(direction);
    }

    private void Start()
    {
        Destroy(gameObject, timeUntilAutoDestroy);
    }

    protected virtual void Update()
    {
        transform.Translate(Vector3.right * (speed * Time.deltaTime));
    }

    protected virtual void OnProjectileHit(Entity entityHit)
    {
        entityHit.TakeDamage(damage);
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag(tag) && other.TryGetComponent(out Entity isEntity))
        {
            OnProjectileHit(isEntity);
        }
    }
}
