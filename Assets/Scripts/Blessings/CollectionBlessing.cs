using UnityEngine;

public class CollectionBlessing : Blessing
{
    public float collectionModifierChange;
    public override void ApplyBlessing()
    {
        player.collectionModifier += collectionModifierChange; //linear addition
    }
}
