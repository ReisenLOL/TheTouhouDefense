using UnityEngine;

public class PotencyBlessing : Blessing
{
    public float attackModifierChange;
    public override void ApplyBlessing()
    {
        player.damageModifier += attackModifierChange; //linear addition
    }
}
