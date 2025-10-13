using System;
using System.Collections;
using TMPro;
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
    public float waveStartDelay;
    public int currentRound;

    [System.Serializable]
    public class RoundData
    {
        public WaveData[] waves;
    }

    [System.Serializable]
    public class WaveData
    {
        public Enemy[] enemiesToSpawn;
        public int amountToSpawn;
    }

    public RoundData[] rounds;
    public GameObject[] endOfRoundUI;
    public CentralBuilding centralBuilding;
    public TextMeshProUGUI waveText;
    public TextMeshProUGUI roundText;
    public float fadeDuration;

    private void Start()
    {
        SpawnNextRound();
    }

    [ContextMenu("Spawn Round")]
    public void SpawnNextRound()
    {
        foreach (GameObject endOfRoundRObjects in endOfRoundUI)
        {
            endOfRoundRObjects.SetActive(false);
        }

        StartCoroutine(SpawnRound(rounds[currentRound]));
        roundText.text = $"Round {currentRound+1}";
        StartCoroutine(FadeText(roundText));
        waveText.text = $"Wave 1";
        StartCoroutine(FadeText(waveText));
    }

    IEnumerator SpawnRound(RoundData round)
    {
        int currentWave = 0;
        while (currentWave < round.waves.Length)
        {
            int currentEnemyNumber = 0;
            while (currentEnemyNumber < round.waves[currentWave].amountToSpawn)
            {
                int randomEnemy = Random.Range(0, round.waves[currentWave].enemiesToSpawn.Length);
                Enemy newEnemy = Instantiate(round.waves[currentWave].enemiesToSpawn[randomEnemy], enemiesFolder);
                newEnemy.transform.position = spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position;
                currentEnemyNumber++;
                yield return new WaitForSeconds(timeBetweenEnemySpawns);
            }

            while (activeEnemyCount > 0)
            {
                yield return null;
            }
            centralBuilding.health += centralBuilding.endOfWaveHealing;
            currentWave++;
            if (currentWave < round.waves.Length)
            {
                waveText.text = $"Wave {currentWave+1}";
                StartCoroutine(FadeText(waveText));
            }
            yield return new WaitForSeconds(waveStartDelay);
        }
        currentRound++;
        EndOfRound();
        yield break;
    }

    public void EndOfRound()
    {
        BlessingSelector.instance.RefreshList(true);
        centralBuilding.health += centralBuilding.endOfRoundHealing;
        roundText.text = $"End of Round {currentRound}";
        StartCoroutine(FadeText(roundText));
        foreach (GameObject endOfRoundRObjects in endOfRoundUI)
        {
            endOfRoundRObjects.SetActive(true);
        }
    }

    IEnumerator FadeText(TextMeshProUGUI text)
    {
        text.gameObject.SetActive(true);
        float timeElapsed = 0f;
        while (timeElapsed < fadeDuration)
        {
            timeElapsed += Time.deltaTime;
            text.color = Color.Lerp(Color.white, Color.clear, timeElapsed / fadeDuration);
            yield return null;
        }
        text.gameObject.SetActive(false);
        yield break;
    }
}
