using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> : ObjectPoolBase<T> where T : Component
{
    private readonly T prefab;
    private readonly Queue<T> objects = new Queue<T>();

    public ObjectPool(T prefab, int initialSize)
    {
        this.prefab = prefab;
        for (int i = 0; i < initialSize; i++)
        {
            T obj = GameObject.Instantiate(prefab);
            obj.gameObject.SetActive(false);
            objects.Enqueue(obj);
        }
    }

    public override T Get()
    {
        if (objects.Count == 0)
        {
            AddObjects(1);
        }

        T obj = objects.Dequeue();
        obj.gameObject.SetActive(true);
        return obj;
    }

    public override void ReturnToPool(T obj)
    {
        obj.gameObject.SetActive(false);
        objects.Enqueue(obj);
    }

    private void AddObjects(int count)
    {
        for (int i = 0; i < count; i++)
        {
            T newObj = GameObject.Instantiate(prefab);
            newObj.gameObject.SetActive(false);
            objects.Enqueue(newObj);
        }
    }
}
