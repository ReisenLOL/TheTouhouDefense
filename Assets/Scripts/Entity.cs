using UnityEngine;

public class Entity : MonoBehaviour
{
    [Header("HEALTH")] 
    public float health;
    public float maxHealth;
    public bool invulnerable;
    public void TakeDamage(float damage)
    {
        if (!invulnerable)
        {
            health -= damage;
            if (health <= 0)
            {
                OnKillEffects();
            }
        }
    }
    protected virtual void OnKillEffects()
    {
        Destroy(gameObject);
    }
}
