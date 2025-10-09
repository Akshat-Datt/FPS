using UnityEngine;
using System.Collections.Generic;

public class ProjectilePool : MonoBehaviour
{
    public static ProjectilePool Instance { get; private set; }

    [System.Serializable]
    public class PoolItem
    {
        public string name;
        public GameObject prefab;
        public int initialSize = 10;
    }

    [SerializeField] private List<PoolItem> projectileTypes;
    private Dictionary<string, ObjectPoolBase<ProjectileBase>> pools;


   private void Awake()
{
    if (Instance == null)
        Instance = this;
    else
    {
        Destroy(gameObject);
        return;
    }

    if (pools == null)
        pools = new Dictionary<string, ObjectPoolBase<ProjectileBase>>();

    if (projectileTypes == null)
        projectileTypes = new List<PoolItem>();

    foreach (var type in projectileTypes)
    {
        if (type.prefab == null)
        {
            Debug.LogWarning($"Projectile prefab not assigned for type: {type.name}");
            continue;
        }

        ProjectileBase projectilePrefab = type.prefab.GetComponent<ProjectileBase>();

        if (projectilePrefab == null)
        {
            Debug.LogError($"Prefab '{type.prefab.name}' does not have a ProjectileBase-derived component attached!");
            continue;
        }

        var pool = new ObjectPool<ProjectileBase>(projectilePrefab, type.initialSize);
        pools.Add(type.name, pool);
    }
}

    public ProjectileBase Get(string name)
    {
        if (pools.ContainsKey(name))
            return pools[name].Get();

        Debug.LogWarning($"No projectile pool found for {name}");
        return null;
    }

    public void ReturnToPool(string name, ProjectileBase projectile)
    {
        if (pools.ContainsKey(name))
            pools[name].ReturnToPool(projectile);
        else
            Destroy(projectile.gameObject);
    }
}
