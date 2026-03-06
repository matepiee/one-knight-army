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
    //WaveCounterCanvas
    public Canvas WaveCounterCanvas;
    public TMP_Text WaveCounterText;
    //WinCanvas
    public GameObject WinCanvas;
    public CanvasGroup WinCanvasGroup;
    //EnemyCounterCanvas
    public GameObject EnemyCounterCanvas;
    public CanvasGroup EnemyCounterCanvasGroup;
    public TMP_Text EnemyCounterText;

    private float timer;
    private int currentEnemyIndex;
    private bool isWaveActive = false;
    private List<GameObject> activeEnemies = new List<GameObject>();
    private bool winShown = false;

    void Start()
    {
        if (waves.Length > 0)
            timer = waves[currentWaveIndex].spawnRate;
    }

    void Update()
    {
        activeEnemies.RemoveAll(enemy => enemy == null);

        // Gomb kezelése: Csak ha nincs aktív hullám és nincs élõ ellenség
        if (!isWaveActive && activeEnemies.Count == 0)
        {
            // Ha elfogytak a hullámok, ne jelenjen meg többé a gomb
            if (currentWaveIndex < waves.Length)
            {
                StartButton.gameObject.SetActive(true);
                StartButton.interactable = true;
                WaveCounterText.text = "Preperation";
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

        // Ha az összes ellenség kijött az ADOTT hullámból
        if (currentEnemyIndex >= currentWave.enemies.Length)
        {
            isWaveActive = false;
            currentWaveIndex++;
            Debug.Log("A hullám összes szörnye spawnolt.");
        }
    }
}