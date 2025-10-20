using UnityEngine;

public class HealingBlessing : Blessing
{
    public float healingAmountToAdd;
    public override void ApplyBlessing()
    {
        player.healingAmount += healingAmountToAdd;
    }
}
