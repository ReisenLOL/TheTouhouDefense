using System;
using UnityEngine;

public class Blessing : MonoBehaviour
{
    public float tier;
    public float cost;
    public string blessingID;
    public PlayerController player;
    public bool canStack;
    public float stackAmount = 1f;
    public float stackLimit;

    public virtual void ApplyBlessing()
    {
        
    }
}
