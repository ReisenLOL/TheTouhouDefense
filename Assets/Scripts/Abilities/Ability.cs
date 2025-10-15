using System;
using UnityEngine;

public class Ability : MonoBehaviour
{
    public string abilityID;
    public bool onCooldown;
    public float baseCooldownLength;
    public float calculatedCooldown;
    public float currentCooldownTime;
    public bool isPrimaryAttack;
    public AudioClip abilitySound;
    public float abilityVolume;
    public PlayerController thisPlayer;
    public static event Action OnAbilityActivated;
    
    private void Start()
    {
        calculatedCooldown = baseCooldownLength;
    }

    public virtual void ActivateAbility()
    {
        if (!onCooldown)
        {
            onCooldown = true;
            currentCooldownTime = calculatedCooldown;
            AbilityEffects();
            if (abilitySound)
            {
                thisPlayer.audioSource.PlayOneShot(abilitySound, abilityVolume);   
            }
            //new thing learnt: events?
            if (!isPrimaryAttack)
            {
                OnAbilityActivated?.Invoke();
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
