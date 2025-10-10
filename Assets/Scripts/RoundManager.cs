using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoundManager : MonoBehaviour
{
    #region Statication
    public static RoundManager instance;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        instance = this;
    }
    #endregion

    public int activeEnemyCount;
    public Transform[] spawnPoints;
    public Transform enemiesFolder;
    public float timeBetweenEnemySpawns;
    public int currentRound;
    [System.Serializable]
    public class RoundData
    {
        public WaveData[] waves;
    }
    [System.Serializable]
    public class WaveData
    {
        public Enemy enemyToSpawn;
        public int amountToSpawn;
    }

    public RoundData[] rounds;
    [ContextMenu("Spawn Round")]
    public void SpawnNextRound()
    {
        StartCoroutine(SpawnRound(rounds[currentRound]));
    }
    IEnumerator SpawnRound(RoundData round)
    {
        int currentWave = 0;
        while (currentWave < round.waves.Length)
        {
            int currentEnemyNumber = 0;
            while (currentEnemyNumber < round.waves[currentWave].amountToSpawn)
            {
                Enemy newEnemy = Instantiate(round.waves[currentWave].enemyToSpawn, enemiesFolder);
                newEnemy.transform.position = spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position;
                currentEnemyNumber++;
                yield return new WaitForSeconds(timeBetweenEnemySpawns);
            }
            while (activeEnemyCount > 0)
            {
                yield return null;
            }
            currentWave++;
        }
        currentRound++;
        yield break;
    } 
}
