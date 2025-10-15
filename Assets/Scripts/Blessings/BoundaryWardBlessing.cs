using System;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryWardBlessing : Blessing
{
    public BoundaryWardRange boundaryRange;
    public BoundaryWardRange boundaryRangeInstance;
    public List<Enemy> enemiesInRange = new();
    public float damage;
    public float damageRate;
    private float currentTime;
    public float radius;
    public override void ApplyBlessing()
    {
        if (boundaryRangeInstance)
        {
            Destroy(boundaryRangeInstance.gameObject);
        }
        boundaryRangeInstance = Instantiate(boundaryRange, player.centralBuilding.transform.position, boundaryRange.transform.rotation);
        boundaryRangeInstance.thisBlessing = this;
        boundaryRangeInstance.transform.localScale = new Vector3(radius, radius);
    }

    private void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime > damageRate)
        {
            currentTime -= damageRate;
            foreach (Enemy enemyFound in enemiesInRange)
            {
                enemyFound.TakeDamage(damage);
            }
        }
    }
}
