using UnityEngine;
using System.Collections.Generic;

public class ProjectilePool : MonoBehaviour
{
    public static ProjectilePool Instance { get; private set; }

    [System.Serializable]
    public class PoolItem
    {
        public string name;
        public ProjectileBase prefab;
        public int initialSize = 10;
    }

    [SerializeField] private List<PoolItem> projectileTypes = new List<PoolItem>();
    private readonly Dictionary<string, ObjectPoolBase<ProjectileBase>> pools = new();


    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        foreach (var type in projectileTypes)
        {
            ObjectPoolBase<ProjectileBase> pool = new ObjectPool<ProjectileBase>(type.prefab, type.initialSize);
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
