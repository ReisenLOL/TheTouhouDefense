using UnityEngine;

public class Equippable : MonoBehaviour
{
    [Header("Identification")]
    public string ID;
    public string description;
    public float tier;
    public float cost;
    [Header("Stats")]
    public bool canStack;
    public float stackAmount = 1f;
    public float stackLimit;
}
