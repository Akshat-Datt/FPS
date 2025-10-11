using UnityEngine;
using System.Collections;

public class ParticlePool : MonoBehaviour
{
    public static ParticlePool Instance { get; private set; }

    [SerializeField] private ParticleSystem explosionPrefab;
    [SerializeField] private ParticleSystem muzzlePrefab;
    [SerializeField] private int poolSize = 10;

    private ObjectPool<ParticleSystem> explosionPool;
    private ObjectPool<ParticleSystem> muzzlePool;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        explosionPool = new ObjectPool<ParticleSystem>(explosionPrefab, poolSize);
        muzzlePool = new ObjectPool<ParticleSystem>(muzzlePrefab, poolSize);
    }

    public void PlayExplosion(Vector3 position)
    {
        ParticleSystem ps = explosionPool.Get();
        ps.transform.position = position;
        ps.Play();
        StartCoroutine(ReturnAfter(ps, ps.main.duration));
    }

    public void PlayMuzzle(Vector3 position, Quaternion rotation)
    {
        ParticleSystem ps = muzzlePool.Get();
        ps.transform.position = position;
        ps.transform.rotation = rotation;
        ps.Play();
        StartCoroutine(ReturnAfter(ps, ps.main.duration));
    }

    private IEnumerator ReturnAfter(ParticleSystem ps, float delay)
    {
        yield return new WaitForSeconds(delay);
        ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);

        if (ps == explosionPrefab) explosionPool.ReturnToPool(ps);
        else muzzlePool.ReturnToPool(ps);
    }
}
