using UnityEngine;

public class DefenseBlessing : Blessing
{
    public float defenseToAdd;
    public override void ApplyBlessing()
    {
        player.defense += defenseToAdd;
    }
}
