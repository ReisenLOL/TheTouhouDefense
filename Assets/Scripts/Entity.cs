using UnityEngine;

public class Entity : MonoBehaviour
{
    [Header("HEALTH")] 
    public float health;
    public float maxHealth;
    public float defense;
    public bool invulnerable;
    public virtual void TakeDamage(float damage)
    {
        if (!invulnerable)
        {
            health -= damage - defense; //unsure if it should be a percentage or a flat value.
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
