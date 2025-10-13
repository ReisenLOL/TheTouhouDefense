using System;
using UnityEngine;

public class SanctuaryBlessing : Blessing
{
    public float baseInvulnerabilityLength;
    public float currentInvulnerabilityLength;
    private float currentTime;
    public bool isActive;
    private void Start()
    {
        Ability.OnAbilityActivated += InvulnerabilityEffect;
    }

    public override void ApplyBlessing()
    {
        currentInvulnerabilityLength = baseInvulnerabilityLength + baseInvulnerabilityLength * (1 - 1 / (1 + baseInvulnerabilityLength * stackAmount));
    }
    private void InvulnerabilityEffect()
    {
        currentTime += currentInvulnerabilityLength; //allows for player to double invulnerability time
        player.invulnerable = true;
        isActive = true;
    }

    private void Update()
    {
        if (isActive)
        {
            currentTime -= Time.deltaTime;
            if (currentTime < 0)
            {
                player.invulnerable = false;
                isActive = false;
            }
        }
    }
}
