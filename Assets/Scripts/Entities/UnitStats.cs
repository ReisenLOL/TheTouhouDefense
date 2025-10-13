using UnityEngine;

[CreateAssetMenu(fileName = "New Unit Stats", menuName = "Unit/UnitStats")]
public class UnitStats : ScriptableObject
{
    public float speed;
    public float damage;
    public float fireRate;
    public float projectileSpeed;
}
