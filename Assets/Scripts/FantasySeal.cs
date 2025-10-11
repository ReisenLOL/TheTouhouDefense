using UnityEngine;

public class FantasySeal : Ability
{
    public Transform[] bulletSpawnPoints;
    public Projectile projectile;
    public float damageModifier;
    
    protected override void AbilityEffects()
    {
        for (int i = 0; i < bulletSpawnPoints.Length; i++)
        {
            FireProjectile(bulletSpawnPoints[i]);
        }
    }
    private void FireProjectile(Transform spawnPosition)
    {
        Projectile spawnedAttack = Instantiate(projectile, transform.position, projectile.transform.rotation);
        spawnedAttack.RotateToTarget(spawnPosition.position);
        spawnedAttack.damage = thisPlayer.baseDamage * damageModifier;
        spawnedAttack.tag = "Friendly";
    }
}
