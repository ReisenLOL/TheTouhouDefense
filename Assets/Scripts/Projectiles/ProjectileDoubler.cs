using UnityEngine;

public class ProjectileDoubler : Projectile
{
    public Projectile originalProjectile;
    public Transform[] creationPoints;
    protected override void Start()
    {
        originalProjectile = ResourceManager.instance.player.primaryAttackInstance.originalProjectile;
        float projectileSpread = ResourceManager.instance.player.primaryAttackInstance.projectileSpread;
        timeUntilAutoDestroy = originalProjectile.timeUntilAutoDestroy;
        base.Start();
        speed = originalProjectile.speed;
        foreach (Transform point in creationPoints)
        {
            Projectile newProjectile = Instantiate(originalProjectile, point);
            newProjectile.tag = "Friendly";
            newProjectile.damage = damage;
            newProjectile.transform.Rotate(0, 0, Random.Range(-projectileSpread, projectileSpread));
            newProjectile.timeUntilAutoDestroy = timeUntilAutoDestroy; //this is already destroying itself on start but better to do it than to not incase i have a unit that changes this.
        }
    }
}
