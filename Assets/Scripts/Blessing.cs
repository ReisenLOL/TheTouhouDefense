using System;
using UnityEngine;

public class Blessing : MonoBehaviour
{
    public float tier;
    public float cost;
    public string blessingID;
    protected PlayerController player;
    public bool canStack;
    public float stackAmount;
    public float stackLimit;
    private void Start()
    {
        player = GetComponentInParent<PlayerController>();
    }

    public virtual void ApplyBlessing()
    {
        
    }
}
