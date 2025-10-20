using UnityEngine;

public class MaxHealthBlessing : Blessing
{
    public float healthToAdd;
    public override void ApplyBlessing()
    {
        player.maxHealth += healthToAdd;
        player.Heal(healthToAdd);
    }
}
