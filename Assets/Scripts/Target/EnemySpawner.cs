using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    [SerializeField] private Transform[] spawnPoints;   
    [SerializeField] private float spawnInterval = 3f;  
    [SerializeField] private int maxEnemies = 10;       

    private int activeEnemyCount = 0;

    private void Start()
    {
        if (spawnPoints.Length == 0)
            Debug.LogWarning("No spawn points assigned in EnemySpawner!");

        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            // Check if we can spawn more enemies
            if (activeEnemyCount < maxEnemies)
            {
                SpawnEnemy();
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnEnemy()
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        var enemy = EnemyPool.Instance.SpawnEnemy(spawnPoint.position, spawnPoint.rotation);

        activeEnemyCount++;

        // enemy. += () => activeEnemyCount--;
    }

    public void SpawnWave(int count)
    {
        for (int i = 0; i < count; i++)
        {
            SpawnEnemy();
        }
    }
}
