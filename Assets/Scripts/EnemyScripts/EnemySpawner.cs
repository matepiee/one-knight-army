using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

// Ez az osztály tárolja egyetlen hullám adatait
[System.Serializable]
public class Wave
{
    public string waveName;          // A hullám neve (pl. "1. Hullám")
    public GameObject[] enemies;     // Az ebben a hullámban jövõ szörnyek
    public float spawnRate = 2f;     // Milyen gyorsan jöjjenek a szörnyek ebben a hullámban
}

public class EnemySpawner : MonoBehaviour
{
    [Header("Hullámok Beállítása")]
    public Wave[] waves;             // Itt tudod megadni a hullámokat az Inspectorban
    private int currentWaveIndex = 0; // Hányadik hullámnál tartunk

    [Header("Referenciák")]
    public Button StartButton;

    private float timer;
    private int currentEnemyIndex;
    private bool isWaveActive = false;
    private List<GameObject> activeEnemies = new List<GameObject>();

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
            }
        }
        else
        {
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

        Debug.Log(waves[currentWaveIndex].waveName + " elindítva!");
    }

    void SpawnEnemy()
    {
        // Az aktuális hullám adatait használjuk
        Wave currentWave = waves[currentWaveIndex];

        if (currentEnemyIndex < currentWave.enemies.Length)
        {
            GameObject newEnemy = Instantiate(currentWave.enemies[currentEnemyIndex], transform.position, Quaternion.identity);
            activeEnemies.Add(newEnemy);

            currentEnemyIndex++;
            timer = currentWave.spawnRate;
        }

        // Ha az összes ellenség kijött az ADOTT hullámból
        if (currentEnemyIndex >= currentWave.enemies.Length)
        {
            isWaveActive = false;
            currentWaveIndex++; // Felkészülünk a következõ hullámra
            Debug.Log("A hullám összes szörnye spawnolt.");
        }
    }
}