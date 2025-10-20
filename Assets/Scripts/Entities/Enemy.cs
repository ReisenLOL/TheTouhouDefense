using System;
using UnityEngine;

public class Enemy : Unit
{
    protected Transform target;
    protected CentralBuilding centralBuilding;
    protected PlayerController player;
    public Projectile projectile;
    protected float fireRateTimer;
    public float closestTargetRange;
    public float maximumTargetRange;
    public float powerDropped;

    protected virtual void Start()
    {
        player = FindFirstObjectByType<PlayerController>();
        centralBuilding = FindFirstObjectByType<CentralBuilding>();
        target = centralBuilding.transform;
    }

    private void Awake()
    {
        RoundManager.instance.activeEnemyCount++;
    }

    protected virtual void Update()
    {
        //this will be replaced for navmeshes later
        if (player && player.gameObject.activeSelf && Vector3.Distance(player.transform.position, transform.position) <
            Vector3.Distance(centralBuilding.transform.position, transform.position))
        {
            target = player.transform;
        }
        else
        {
            target = centralBuilding.transform;
        }
        HandleAttack();
    }
    
    protected virtual void HandleAttack()
    {
        fireRateTimer += Time.deltaTime;
        if (fireRateTimer > stats.fireRate)
        {
            Projectile newProjectile = Instantiate(projectile, transform.position, projectile.transform.rotation);
            newProjectile.tag = "Enemy";
            newProjectile.RotateToTarget(target.position);
            newProjectile.damage = stats.damage;
            newProjectile.speed = stats.projectileSpeed;
            fireRateTimer = 0;
        }
    }

    protected virtual void FixedUpdate()
    {
        float distanceToTarget = Vector3.Distance(target.transform.position, transform.position);
        if (distanceToTarget < closestTargetRange)
        {
            rb.linearVelocity = -(target.transform.position - transform.position).normalized * stats.speed;
        }
        else if (distanceToTarget >= closestTargetRange && distanceToTarget <= maximumTargetRange)
        {
            rb.linearVelocity = Vector3.zero;
        }
        else
        {
            rb.linearVelocity = (target.transform.position - transform.position).normalized * stats.speed;
        }
    }
    protected override void OnKillEffects()
    {
        base.OnKillEffects();
        ResourceManager.instance.AddPower(powerDropped);
    }

    private void OnDestroy()
    {
        RoundManager.instance.activeEnemyCount--;
    }
}
