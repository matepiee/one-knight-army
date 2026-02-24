using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] EnemyPrefab;
    public float spawnTime = 2;
    private float timer;
    private int currentEnemy;
    void Start()
    {
        timer = spawnTime;
    }

    // Update is called once per frame
    void Update()
    {
        timer-= Time.deltaTime;

        if(timer <= 0)
        {
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        Instantiate(EnemyPrefab[currentEnemy],transform.position,Quaternion.identity);
        currentEnemy++;
        if(currentEnemy >= EnemyPrefab.Length)
            this.enabled = false;
        timer = spawnTime;
    }
}
