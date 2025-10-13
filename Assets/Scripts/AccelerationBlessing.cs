using UnityEngine;

public class AccelerationBlessing : Blessing
{    
    public float attackSpeedChange;
    public override void ApplyBlessing()
    {
        player.attackSpeedModifier += attackSpeedChange; //linear addition
    }
}
