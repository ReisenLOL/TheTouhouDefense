using System;
using UnityEngine;

public class PlayerController : Unit
{
    [Header("Primary Attack")]
    public Projectile primaryProjectile;
    public float fireRate;
    private float fireRateTimer;

    [Header("Stats")] 
    public float baseDamage;
    [Header("Lives")]
    public int lives;
    public float respawnDelay;
    public bool isRespawning;
    [Header("CACHE")]
    private Vector2 moveDirection;
    public Camera cam;
    private Vector3 mouseWorldPos;
    public CentralBuilding centralBuilding;
    
    void Update()
    {
        moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        mouseWorldPos =  cam.ScreenToWorldPoint(Input.mousePosition + new Vector3(0,0,cam.nearClipPlane));
        HandlePrimaryAttack();
    }
    protected override void OnKillEffects()
    {
        gameObject.SetActive(false);
        lives--;
        isRespawning = true;
    }
    public void HandlePrimaryAttack()
    {
        fireRateTimer += Time.deltaTime;
        if (Input.GetMouseButton(0) && fireRateTimer > fireRate && canAttack)
        {
            Projectile newProjectile = Instantiate(primaryProjectile, transform.position, primaryProjectile.transform.rotation);
            newProjectile.damage = baseDamage;
            newProjectile.tag = "Friendly";
            newProjectile.RotateToTarget(mouseWorldPos);
            fireRateTimer = 0;
        }
    }
    private void FixedUpdate()
    {
        if (canMove)
        {
            rb.linearVelocity = moveDirection * speed;
        }
    }
}
