using UnityEngine;

public class SwiftnessBlessing : Blessing
{
    public float speedChange;
    public override void ApplyBlessing()
    {
        player.speedModifier += speedChange; //linear addition
    }
}
