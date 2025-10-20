using UnityEngine;

public class PierceProjectile : Projectile
{
    protected override void OnProjectileHit(Entity entityHit)
    {
        entityHit.TakeDamage(damage);
    }
}
