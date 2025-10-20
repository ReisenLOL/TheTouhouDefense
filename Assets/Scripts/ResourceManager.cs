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
    public PlayerController player;
    public TextMeshProUGUI powerText;
    public TextMeshProUGUI atkText;
    public TextMeshProUGUI atkSpdText;

    private void Start()
    {
        UpdatePowerUI();
    }

    public void AddPower(float power)
    {
        powerStored += power * player.collectionModifier;
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
        powerText.text = MathF.Floor(powerStored).ToString();
        atkText.text = $"+ {Math.Round(((powerStored / powerDivisor) * 100) * 100) / 100}%";
        atkSpdText.text= $"- {Math.Round((((powerStored / powerDivisor) / 2) * 100) * 100) / 100}%";
    }
}

