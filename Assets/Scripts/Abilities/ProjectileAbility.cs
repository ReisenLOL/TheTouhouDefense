using UnityEngine;

public class ProjectileAbility : Ability
{
    public Projectile originalProjectile;
    public Projectile currentProjectile;
    public float damageMultiplier = 1;
    public float projectileSpread;
    protected override void AbilityEffects()
    {
        Projectile newProjectile = Instantiate(currentProjectile, transform.position, currentProjectile.transform.rotation);
        newProjectile.damage = thisPlayer.calculatedDamage * damageMultiplier;
        newProjectile.tag = "Friendly";
        newProjectile.RotateToTarget(thisPlayer.mouseWorldPos);
        newProjectile.transform.Rotate(0, 0, Random.Range(-projectileSpread, projectileSpread));
    }
}
