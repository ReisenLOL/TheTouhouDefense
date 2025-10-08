using UnityEngine;

public class Unit : MonoBehaviour
{
    [Header("STATS")] 
    public float health;
    public float maxHealth;
    public float speed;
    [Header("CACHE")]
    public Rigidbody2D rb;

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            OnKillEffects();
        }
    }

    protected virtual void OnKillEffects()
    {
        
    }
}
