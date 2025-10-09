using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    #region Statication
    public static ResourceManager instance;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        instance = this;
    }
    #endregion
    public float powerStored;

    public void AddPower(float power)
    {
        powerStored += power;
    }
    public bool RemovePower(float power)
    {
        if (powerStored > power)
        {
            powerStored -= power;
            return true;
        }
        return false;
    }
}

