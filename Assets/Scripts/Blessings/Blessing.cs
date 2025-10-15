using System;
using UnityEngine;

public class Blessing : MonoBehaviour
{
    [Header("Identification")]
    public string blessingID;
    public string blessingDescription;
    public float tier;
    [Header("Stats")]
    public float cost;
    public PlayerController player;
    public bool canStack;
    public float stackAmount = 1f;
    public float stackLimit;

    public virtual void ApplyBlessing()
    {
        
    }
}
