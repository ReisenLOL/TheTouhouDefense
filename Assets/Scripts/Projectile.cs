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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag(tag) && other.TryGetComponent(out Entity isEntity))
        {
            isEntity.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
