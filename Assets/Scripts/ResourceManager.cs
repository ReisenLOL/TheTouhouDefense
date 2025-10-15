using System;
using TMPro;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    #region Statication
    public static ResourceManager instance;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    #endregion
    public float powerStored;
    public float powerDivisor;
    public TextMeshProUGUI powerText;

    private void Start()
    {
        UpdatePowerUI();
    }

    public void AddPower(float power)
    {
        powerStored += power;
        UpdatePowerUI();
    }
    public bool RemovePower(float power)
    {
        if (powerStored >= power)
        {
            powerStored -= power;
            UpdatePowerUI();
            return true;
        }
        return false;
    }

    public void UpdatePowerUI()
    {
        powerText.text = $"Power: {MathF.Floor(powerStored)}\n(Bonus from power: +{Math.Round(((powerStored/powerDivisor)*100)*100)/100}% Damage, +{Math.Round((((powerStored/powerDivisor)/2)*100)*100)/100}% Attack Speed)";
    }
}

