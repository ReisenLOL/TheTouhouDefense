using UnityEngine;

public class Unit : Entity
{

    public float speed;
    [Header("Flags")]
    public bool canAttack = true;
    public bool canMove = true;
    [Header("CACHE")]
    public Rigidbody2D rb;
    
}
