using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Unit
{
    [Header("Primary Attack")]
    public Projectile primaryProjectile;
    public float fireRate;
    private float fireRateTimer;

    [Header("Abilities")] 
    public Ability ability1;
    private Ability ability1Instance;
    public Ability ability2;
    private Ability ability2Instance;
    [Header("Stats")] 
    public float baseDamage;
    public float speed;
    [Header("Modifiers")]
    public float damageModifier = 1f;
    public float attackSpeedModifier = 1f;
    public float speedModifier = 1f;
    public float collectionModifier = 1f;
    public float ability1CooldownModifier;
    public float ability2CooldownModifier;
    [Header("Lives")]
    public int lives;
    public float respawnDelay;
    public bool isRespawning;
    [Header("CACHE")]
    public List<Blessing> equippedBlessings = new();
    public float calculatedDamage;
    private Vector2 moveDirection;
    public Camera cam;
    private Vector3 mouseWorldPos;
    public CentralBuilding centralBuilding;

    private void Start()
    {
        ability1Instance = Instantiate(ability1, transform);
        ability2Instance = Instantiate(ability2, transform);
    }
    void Update()
    {
        calculatedDamage = baseDamage * damageModifier * 1f + ResourceManager.instance.powerStored/ResourceManager.instance.powerDivisor;
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
            newProjectile.damage = calculatedDamage;
            newProjectile.tag = "Friendly";
            newProjectile.RotateToTarget(mouseWorldPos);
            fireRateTimer = 0;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ability1Instance.ActivateAbility();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            ability2Instance.ActivateAbility();
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
