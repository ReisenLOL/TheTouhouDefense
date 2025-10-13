using UnityEngine;

public class SealingTalisman : Ability
{
    public Projectile projectile;
    protected override void AbilityEffects()
    {
        Projectile newProjectile = Instantiate(projectile, transform.position, projectile.transform.rotation);
        newProjectile.damage = thisPlayer.calculatedDamage;
        newProjectile.tag = "Friendly";
        newProjectile.RotateToTarget(thisPlayer.mouseWorldPos);
    }
}
