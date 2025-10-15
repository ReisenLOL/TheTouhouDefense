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
    [Header("Healing")] 
    public float healingAmount;
    public float healingTime;
    private float currentHealingTime;
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
        primaryAttackInstance.thisPlayer = this;
        primaryAttackIcon.targetAbility = primaryAttackInstance;
        ability1Instance = Instantiate(ability1, transform);
        ability1Instance.thisPlayer = this;
        ability1Icon.targetAbility = ability1Instance;
        ability2Instance = Instantiate(ability2, transform);
        ability2Instance.thisPlayer = this;
        ability2Icon.targetAbility = ability2Instance;
    }
    void Update()
    {
        moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        mouseWorldPos =  cam.ScreenToWorldPoint(Input.mousePosition + new Vector3(0,0,cam.nearClipPlane));
        CalculateStats();
        HandlePrimaryAttack();
        HandleHealing();
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
    public override void Heal(float healing)
    {
        base.Heal(healing);
        UpdateHealthBar();
    }
    protected override void OnKillEffects()
    {
        gameObject.SetActive(false);
        lives--;
        isRespawning = true;
    }

    private void CalculateStats()
    {
        calculatedDamage = baseDamage * damageModifier * (1f + ResourceManager.instance.powerStored/ResourceManager.instance.powerDivisor);
        calculatedAttackSpeed = fireRate * attackSpeedModifier * (1f - ((ResourceManager.instance.powerStored / ResourceManager.instance.powerDivisor)/2));
        primaryAttackInstance.calculatedCooldown = calculatedAttackSpeed;
        calculatedSpeed = speed * speedModifier;
    }
    private void HandleHealing()
    {
        currentHealingTime += Time.deltaTime;
        if (currentHealingTime > healingTime)
        {
            currentHealingTime -= healingTime;
            Heal(healingAmount);
        }
    }
    private void HandlePrimaryAttack()
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
