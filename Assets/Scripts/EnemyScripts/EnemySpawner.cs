using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Wave
{
    public string waveName;
    public GameObject[] enemies;
    public float spawnRate = 2f;
}

public class EnemySpawner : MonoBehaviour
{
    [Header("Waves")]
    public Wave[] waves;
    private int currentWaveIndex = 0;

    [Header("References")]
    public Button StartButton;
    public Transform enemyParent;
    public Canvas WaveCounterCanvas;
    public TMP_Text WaveCounterText;
    public GameObject WinCanvas;
    public CanvasGroup WinCanvasGroup;
    public GameObject EnemyCounterCanvas;
    public CanvasGroup EnemyCounterCanvasGroup;
    public TMP_Text EnemyCounterText;

    private float timer;
    private int currentEnemyIndex;
    private bool isWaveActive = false;
    private List<GameObject> activeEnemies = new List<GameObject>();
    private bool winShown = false;

    private bool isPreparationPhase = true;

    void Start()
    {
        if (waves.Length > 0)
            timer = waves[currentWaveIndex].spawnRate;

        isPreparationPhase = true;
    }

    void Update()
    {
        activeEnemies.RemoveAll(enemy => enemy == null);

        if (!isWaveActive && activeEnemies.Count == 0)
        {
            if (currentWaveIndex < waves.Length && currentEnemyIndex >= waves[currentWaveIndex].enemies.Length)
            {
                currentWaveIndex++;
                currentEnemyIndex = 0;
            }

            if (currentWaveIndex < waves.Length)
            {
                if (!isPreparationPhase)
                {
                    isPreparationPhase = true;
                    if (MusicManager.instance != null)
                    {
                        MusicManager.instance.PlayPreparationMusic();
                    }
                }

                StartButton.gameObject.SetActive(true);
                StartButton.interactable = true;
                WaveCounterText.text = "Preparation";
                EnemyCounterCanvasGroup.alpha = 0;
            }
            else if (currentWaveIndex >= waves.Length && !winShown)
            {
                winShown = true;

                StartButton.gameObject.SetActive(false);

                WinCanvas.SetActive(true);
                WinCanvasGroup.alpha = 1;
                WinCanvasGroup.blocksRaycasts = true;
                WinCanvasGroup.interactable = true;
            }
        }
        else
        {
            EnemyCounterCanvasGroup.alpha = 1;
            EnemyCounterText.text = waves[currentWaveIndex].enemies.Length + "/" + activeEnemies.Count.ToString();
            StartButton.gameObject.SetActive(false);
        }

        if (!isWaveActive) return;

        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            SpawnEnemy();
        }
    }

    public void WaveStart()
    {
        if (isWaveActive || activeEnemies.Count > 0 || currentWaveIndex >= waves.Length) return;

        currentEnemyIndex = 0;
        timer = 0;
        isWaveActive = true;
        WaveCounterText.text = waves[currentWaveIndex].waveName;
        isPreparationPhase = false;
        if (MusicManager.instance != null)
        {
            MusicManager.instance.PlayGameMusic();
        }

        Debug.Log(waves[currentWaveIndex].waveName + " elindítva!");
    }

    void SpawnEnemy()
    {
        Wave currentWave = waves[currentWaveIndex];

        if (currentEnemyIndex < currentWave.enemies.Length)
        {
            GameObject newEnemy = Instantiate(currentWave.enemies[currentEnemyIndex], transform.position, Quaternion.identity, enemyParent);
            activeEnemies.Add(newEnemy);

            currentEnemyIndex++;
            timer = currentWave.spawnRate;
        }

        if (currentEnemyIndex >= currentWave.enemies.Length)
        {
            isWaveActive = false;
            Debug.Log("A hullám összes szörnye spawnolt.");
        }
    }
}