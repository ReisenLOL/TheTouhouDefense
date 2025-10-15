using UnityEngine;

public class FantasySeal : Ability
{
    public Transform[] bulletSpawnPoints;
    public HomingProjectile projectile;
    [Header("Projectile Stats")]
    public float damageModifier;
    public float projectileSpeed;
    public float homingRadius;
    
    protected override void AbilityEffects()
    {
        for (int i = 0; i < bulletSpawnPoints.Length; i++)
        {
            FireProjectile(bulletSpawnPoints[i]);
        }
    }
    private void FireProjectile(Transform spawnPosition)
    {
        HomingProjectile spawnedAttack = Instantiate(projectile, transform.position, projectile.transform.rotation);
        spawnedAttack.GetComponent<SpriteRenderer>().color = Random.ColorHSV();
        spawnedAttack.RotateToTarget(spawnPosition.position);
        spawnedAttack.damage = thisPlayer.calculatedDamage * damageModifier;
        spawnedAttack.speed =  projectileSpeed;
        spawnedAttack.homingRadius = homingRadius;
        spawnedAttack.tag = "Friendly";
    }
}
