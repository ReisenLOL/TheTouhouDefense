using System;
using UnityEngine;

public class PlayerController : Unit
{
    private Vector2 moveDirection;
    public Projectile primaryProjectile;
    public float fireRate;
    private float fireRateTimer;
    public Camera cam;
    private Vector3 mouseWorldPos;
    
    void Update()
    {
        moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        mouseWorldPos =  cam.ScreenToWorldPoint(Input.mousePosition + new Vector3(0,0,cam.nearClipPlane));
        HandlePrimaryAttack();
    }
    protected override void OnKillEffects()
    {
        
    }
    public void HandlePrimaryAttack()
    {
        fireRateTimer += Time.deltaTime;
        if (Input.GetMouseButton(0) && fireRateTimer > fireRate)
        {
            Projectile newProjectile = Instantiate(primaryProjectile, transform.position, primaryProjectile.transform.rotation);
            newProjectile.tag = "Friendly";
            newProjectile.RotateToTarget(mouseWorldPos);
            fireRateTimer = 0;
        }
    }
    private void FixedUpdate()
    {
        rb.linearVelocity = moveDirection * speed;
    }
}
