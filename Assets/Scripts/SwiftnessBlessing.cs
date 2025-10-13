using UnityEngine;

public class SwiftnessBlessing : Blessing
{
    public float speedChange;
    public override void ApplyBlessing()
    {
        player.attackSpeedModifier += speedChange; //linear addition
    }
}
