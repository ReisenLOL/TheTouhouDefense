using System;
using UnityEngine;

public class WallUpgrade : CentralUpgrade
{
    public Entity wall;
    public Entity wallInstance;
    public float healthToAdd;
    private void Start()
    {
        wallInstance = Instantiate(wall, centralBuilding.transform.position, centralBuilding.transform.rotation);
    }

    public override void ApplyUpgrade()
    {
        wallInstance.maxHealth += healthToAdd;
        wallInstance.Heal(healthToAdd);
    }
}
