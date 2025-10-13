using System;
using TMPro;
using UnityEngine;

public class AbilityIcon : MonoBehaviour
{
    public Ability targetAbility;
    public GameObject overlay;
    public TextMeshProUGUI timer;
    public TextMeshProUGUI namePlaceholder;
    private void Start()
    {
        namePlaceholder.text = targetAbility.abilityID;
    }

    private void Update()
    {
        if (targetAbility.onCooldown)
        {
            if (!overlay.activeSelf)
            {
                overlay.SetActive(true);   
            }
            timer.text = (MathF.Round(targetAbility.currentCooldownTime*100f)/100f).ToString();
        }
        else
        {
            overlay.SetActive(false);
        }
    }
}
