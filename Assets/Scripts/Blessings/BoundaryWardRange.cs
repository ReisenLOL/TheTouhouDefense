using System;
using UnityEngine;

public class BoundaryWardRange : MonoBehaviour
{
    public BoundaryWardBlessing thisBlessing;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") && other.TryGetComponent(out Enemy isEnemy) && !thisBlessing.enemiesInRange.Contains(isEnemy))
        {
            thisBlessing.enemiesInRange.Add(isEnemy);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") && other.TryGetComponent(out Enemy isEnemy) && thisBlessing.enemiesInRange.Contains(isEnemy))
        {
            thisBlessing.enemiesInRange.Remove(isEnemy);
        }
    }
}
