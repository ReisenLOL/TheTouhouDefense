using UnityEngine;

public class DuplexAttackBlessing : Blessing
{
    public ProjectileDoubler projectileDoubler;
    public override void ApplyBlessing()
    {
        ResourceManager.instance.player.primaryAttackInstance.currentProjectile = projectileDoubler;
    }
}
