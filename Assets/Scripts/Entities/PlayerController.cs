using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : Unit
{
    [Header("Abilities")] 
    public Ability primaryAttack;
    public Ability primaryAttackInstance;
    public AbilityIcon primaryAttackIcon;
    public Ability ability1;
    public Ability ability1Instance;
    public AbilityIcon ability1Icon;
    public Ability ability2;
    public Ability ability2Instance;
    public AbilityIcon ability2Icon;
    [Header("Stats")] 
    public float baseDamage;
    public float fireRate;
    public float speed;
    [Header("Calculated Stats")]
    public float calculatedDamage;
    public float calculatedAttackSpeed;
    public float calculatedSpeed;
    [Header("Modifiers")]
    public float damageModifier = 1f;
    public float attackSpeedModifier = 1f;
    public float speedModifier = 1f;
    public float collectionModifier = 1f;
    [Header("Lives")]
    public int lives;
    public float respawnDelay;
    public bool isRespawning;
    [Header("CACHE")]
    public List<Blessing> equippedBlessings = new();
    private Vector2 moveDirection;
    public Camera cam;
    public Vector3 mouseWorldPos;
    public CentralBuilding centralBuilding;
    public Transform healthBar;
    public TextMeshProUGUI healthBarText;
    public AbilityIcon templateIcon;
    public Transform iconGrid;

    private void Awake()
    {
        primaryAttackInstance = Instantiate(primaryAttack, transform);
        primaryAttackInstance.baseCooldownLength = fireRate;
        primaryAttackIcon.targetAbility = primaryAttackInstance;
        ability1Instance = Instantiate(ability1, transform);
        ability1Icon.targetAbility = ability1Instance;
        ability2Instance = Instantiate(ability2, transform);
        ability2Icon.targetAbility = ability2Instance;
    }
    void Update()
    {
        calculatedDamage = baseDamage * damageModifier * (1f + ResourceManager.instance.powerStored/ResourceManager.instance.powerDivisor);
        calculatedAttackSpeed = fireRate * attackSpeedModifier * (1f - ResourceManager.instance.powerStored / ResourceManager.instance.powerDivisor);
        primaryAttackInstance.calculatedCooldown = calculatedAttackSpeed;
        calculatedSpeed = speed * speedModifier;
        moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        mouseWorldPos =  cam.ScreenToWorldPoint(Input.mousePosition + new Vector3(0,0,cam.nearClipPlane));
        HandlePrimaryAttack();
    }

    public void UpdateHealthBar()
    {
        healthBar.localScale = new Vector2(health / maxHealth, 1);
        healthBarText.text = $"{health}/{maxHealth}"; //neat!
    }
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        UpdateHealthBar();
    }
    protected override void OnKillEffects()
    {
        gameObject.SetActive(false);
        lives--;
        isRespawning = true;
    }
    public void HandlePrimaryAttack()
    {
        if (Input.GetMouseButton(0) && canAttack)
        {
            primaryAttackInstance.ActivateAbility();
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
            rb.linearVelocity = moveDirection * calculatedSpeed;
        }
    }
}
