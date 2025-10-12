using UnityEngine;

public class DuplexBarrier : Ability
{
    public float damageModifier;
    public float radius;
    public LayerMask entityLayer;
    public AttackVisual visual;
    protected override void AbilityEffects()
    {
        AttackVisual newVisual = Instantiate(visual, transform.position, visual.transform.rotation);
        newVisual.transform.localScale = new Vector3(radius, radius, radius);
        Collider2D[] targets =  Physics2D.OverlapBoxAll(transform.position, new Vector2(radius,radius), 0, entityLayer);
        foreach (Collider2D target in targets)
        {
            if (!target.CompareTag(thisPlayer.tag))
            {
                target.GetComponent<Entity>().TakeDamage(thisPlayer.baseDamage * damageModifier);
            }
        }
    }
}
