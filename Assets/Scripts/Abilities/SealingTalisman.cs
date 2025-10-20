using UnityEngine;

public class SealingTalisman : Ability
{
    public Projectile projectile;
    public float damageMultiplier = 1;
    protected override void AbilityEffects()
    {
        Projectile newProjectile = Instantiate(projectile, transform.position, projectile.transform.rotation);
        newProjectile.damage = thisPlayer.calculatedDamage * damageMultiplier;
        newProjectile.tag = "Friendly";
        newProjectile.RotateToTarget(thisPlayer.mouseWorldPos);
    }
}
