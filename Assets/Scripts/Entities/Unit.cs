using UnityEngine;

public class Unit : Entity
{

    public UnitStats stats;
    [Header("Flags")]
    public bool canAttack = true;
    public bool canMove = true;
    [Header("CACHE")]
    public Rigidbody2D rb;
    public AudioSource audioSource;
    
}
