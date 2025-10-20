using UnityEngine;

public class OptionsBlessing : Blessing
{
    public override void ApplyBlessing()
    {
        BlessingSelector.instance.optionAmount++;
    }
}
