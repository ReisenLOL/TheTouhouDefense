using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoundManager : MonoBehaviour
{
    public Transform[] spawnPoints;
    public Enemy enemyToSpawn; // we are gonna like, do thee most basic spawn one every few seconds for the first prototype.
    public float timeUntilEnemySpawn;
    public float currentTime;

    private void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime > timeUntilEnemySpawn)
        {
            Enemy newEnemy = Instantiate(enemyToSpawn, spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position, enemyToSpawn.transform.rotation);
            currentTime -= timeUntilEnemySpawn;
        }
    }
}
