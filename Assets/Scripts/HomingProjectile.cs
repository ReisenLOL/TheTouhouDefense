using UnityEngine;

public class HomingProjectile : Projectile
{
    public float timeUntilHoming;
    public float currentTime;
    public float homingRadius;
    public bool isHoming;
    public LayerMask entityLayer;
    public Entity target;

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (target)
        {
            RotateToTarget(target.transform.position);
        }
        else
        {
            currentTime += Time.deltaTime;
            if (currentTime > timeUntilHoming)
            {
                Collider2D[] possibleTargets = Physics2D.OverlapCircleAll(transform.position, homingRadius, entityLayer);
                Collider2D closestTarget = null;
                foreach (Collider2D possibleTarget in possibleTargets)
                {
                    if (!possibleTarget.CompareTag(tag) && (!closestTarget || Vector3.Distance(transform.position,  possibleTarget.transform.position) < Vector3.Distance(transform.position,  closestTarget.transform.position)))
                    {
                        closestTarget = possibleTarget;
                    }
                }

                if (closestTarget)
                {
                    target = closestTarget.GetComponent<Entity>();
                }
            }
        }
    }
}
