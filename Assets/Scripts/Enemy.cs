using System;
using UnityEngine;

public class Enemy : Unit
{
    private Transform target;
    private CentralBuilding centralBuilding;
    private PlayerController player;
    public Projectile projectile;
    public float fireRate;
    private float fireRateTimer;
    public float powerDropped;

    private void Start()
    {
        player = FindFirstObjectByType<PlayerController>();
        centralBuilding = FindFirstObjectByType<CentralBuilding>();
    }

    void Update()
    {
        //this will be replaced for navmeshes later
        if (Vector3.Distance(player.transform.position, transform.position) <
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
    public void HandleAttack()
    {
        fireRateTimer += Time.deltaTime;
        if (fireRateTimer > fireRate)
        {
            Projectile newProjectile = Instantiate(projectile, transform.position, projectile.transform.rotation);
            newProjectile.tag = "Enemy";
            newProjectile.RotateToTarget(target.position);
            fireRateTimer = 0;
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = (target.transform.position - transform.position).normalized * speed;
    }
    protected override void OnKillEffects()
    {
        base.OnKillEffects();
        ResourceManager.instance.AddPower(powerDropped);
    }
}
