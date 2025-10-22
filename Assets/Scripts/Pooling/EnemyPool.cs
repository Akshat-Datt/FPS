using UnityEngine;
using System.Collections.Generic;

public class EnemyPool : MonoBehaviour
{
    public static EnemyPool Instance { get; private set; }

    [System.Serializable]
    public class PoolItem
    {
        public string name;
        public EnemyBase prefab;
        public int initialSize = 5;
    }

    [Header("Enemy Pool Settings")]
    [SerializeField] private List<PoolItem> enemyTypes = new List<PoolItem>();
    private Dictionary<string, ObjectPool<EnemyBase>> pools = new Dictionary<string, ObjectPool<EnemyBase>>();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        foreach (var type in enemyTypes)
        {
            if (type.prefab == null)
            {
                Debug.LogWarning($"Enemy prefab not assigned for pool type: {type.name}");
                continue;
            }

            var pool = new ObjectPool<EnemyBase>(type.prefab, type.initialSize);
            pools.Add(type.name, pool);
        }
    }

    public EnemyBase SpawnEnemy(string typeName, Vector3 position, Quaternion rotation)
    {
        if (!pools.ContainsKey(typeName))
        {
            Debug.LogError($"Enemy type '{typeName}' not found in pool!");
            return null;
        }

        EnemyBase enemy = pools[typeName].Get();
        enemy.transform.position = position;
        enemy.transform.rotation = rotation;
        enemy.gameObject.SetActive(true);

        // Reset health and enable movement
        enemy.currentHealth = enemy.maxHealth;
        enemy.agent.enabled = true;

        return enemy;
    }

    public void ReturnEnemy(string typeName, EnemyBase enemy)
    {
        if (enemy == null) return;

        if (!pools.ContainsKey(typeName))
        {
            Debug.LogError($"Enemy type '{typeName}' not found in pool!");
            Destroy(enemy.gameObject);
            return;
        }

        enemy.agent.enabled = false;
        enemy.gameObject.SetActive(false);
        pools[typeName].ReturnToPool(enemy); // ✅ correct method name
    }
}
