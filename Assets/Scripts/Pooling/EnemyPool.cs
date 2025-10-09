using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    public static EnemyPool Instance { get; private set; }

    [Header("Enemy Pool Settings")]
    [SerializeField] private Damageable enemyPrefab;
    [SerializeField] private int initialPoolSize = 10;

    private ObjectPool<Damageable> pool;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        if (enemyPrefab == null)
        {
            Debug.LogError("Enemy prefab not assigned in EnemyPool!");
            return;
        }

        // Initialize pool
        pool = new ObjectPool<Damageable>(enemyPrefab, initialPoolSize);
    }

    /// <summary>
    /// Spawns an enemy from the pool at the given position
    /// </summary>
    public Damageable SpawnEnemy(Vector3 position, Quaternion rotation)
    {
        Damageable enemy = pool.Get();
        enemy.transform.position = position;
        enemy.transform.rotation = rotation;
        enemy.gameObject.SetActive(true);
        return enemy;
    }

    /// <summary>
    /// Returns enemy to the pool
    /// </summary>
    public void ReturnEnemy(Damageable enemy)
    {
        if (enemy == null) return;
        enemy.gameObject.SetActive(false);
        pool.ReturnToPool(enemy);
    }
}
