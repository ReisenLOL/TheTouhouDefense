using System;
using UnityEngine;

public class Ability : MonoBehaviour
{
    public bool onCooldown;
    public float cooldownLength;
    public float currentCooldownTime;
    public AudioClip abilitySound;
    public float abilityVolume;
    public PlayerController thisPlayer;
    
    private void Start()
    {
        thisPlayer = GetComponentInParent<PlayerController>();
    }

    public virtual void ActivateAbility()
    {
        if (!onCooldown)
        {
            onCooldown = true;
            currentCooldownTime = cooldownLength;
            AbilityEffects();
            if (abilitySound)
            {
                thisPlayer.audioSource.PlayOneShot(abilitySound, abilityVolume);   
            }
        }
    }

    protected virtual void AbilityEffects()
    {
        
    }
    void Update()
    {
        if (onCooldown)
        {
            currentCooldownTime -= Time.deltaTime;
            if (currentCooldownTime <= 0)
            {
                onCooldown = false;
            }
        }
    }
}
