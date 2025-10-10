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
        }
        instance = this;
    }
    #endregion
    public float powerStored;
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
        if (powerStored > power)
        {
            powerStored -= power;
            return true;
        }
        return false;
    }

    public void UpdatePowerUI()
    {
        powerText.text = "Power: " + powerStored;
    }
}

