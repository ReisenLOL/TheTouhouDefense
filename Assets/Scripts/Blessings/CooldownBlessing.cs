using UnityEngine;

public class CooldownBlessing : Blessing
{
    //i'm going to steal a formula from another game for this lol
    public bool isAbility2;
    public Ability affectedAbility;
    public float cooldownChange;
    public override void ApplyBlessing()
    {
        if (isAbility2)
        {
            affectedAbility = player.ability2Instance;
        }
        else
        {
            affectedAbility = player.ability1Instance;
        }
        affectedAbility.calculatedCooldown = affectedAbility.baseCooldownLength - affectedAbility.baseCooldownLength * (1 - 1 / (1 + cooldownChange * stackAmount)); //this becomes a percentage?? i suppose that's fine?. turns out i am an idiot.
    }
}
