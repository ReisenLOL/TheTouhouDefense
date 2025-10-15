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
            return;
        }
        instance = this;
    }

    #endregion
    
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
        public int enemiesPerBurst = 1;
    }
    [Header("Rounds")]
    public RoundData[] rounds;
    public int currentRound;
    public float timeBetweenEnemySpawns;
    public float waveStartDelay;
    public int activeEnemyCount;
    public Transform[] spawnPoints;
    [Header("UI")]
    public GameObject[] endOfRoundUI;
    public TextMeshProUGUI roundText;
    public TextMeshProUGUI waveText;
    public float fadeDuration;
    [Header("Audio")]
    public AudioClip waveStartSFX;
    public float waveStartVolume;
    [Header("CACHE")]
    public PlayerController player;
    public CentralBuilding centralBuilding;
    public Transform enemiesFolder;

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
        player.audioSource.PlayOneShot(waveStartSFX, waveStartVolume);
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
                for (int i = 0;
                     i < round.waves[currentWave].enemiesPerBurst &&
                     currentEnemyNumber < round.waves[currentWave].amountToSpawn;
                     i++)
                {
                    int randomEnemy = Random.Range(0, round.waves[currentWave].enemiesToSpawn.Length);
                    Enemy newEnemy = Instantiate(round.waves[currentWave].enemiesToSpawn[randomEnemy], enemiesFolder);
                    newEnemy.transform.position = spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position;
                    currentEnemyNumber++;
                }
                yield return new WaitForSeconds(timeBetweenEnemySpawns);
            }

            while (activeEnemyCount > 0)
            {
                yield return null;
            }
            centralBuilding.Heal(centralBuilding.endOfWaveHealing);
            currentWave++;
            if (currentWave < round.waves.Length)
            {
                player.audioSource.PlayOneShot(waveStartSFX, waveStartVolume);
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
        centralBuilding.Heal(centralBuilding.endOfRoundHealing);
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
