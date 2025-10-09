using UnityEngine;

public abstract class ObjectPoolBase<T> where T : Component
{
    public abstract T Get();
    public abstract void ReturnToPool(T obj);
}
