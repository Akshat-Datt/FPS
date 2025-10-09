using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    [SerializeField] protected float speed = 10f;
    [SerializeField] protected int damage = 10;
    [SerializeField] protected float lifetime = 5f;
    [SerializeField] protected string projectileName;

    private float timer;

    protected virtual void OnEnable()
    {
        timer = 0f;
    }

    protected virtual void Update()
    {
        Move();
        timer += Time.deltaTime;
        if (timer >= lifetime)
            ReturnToPool();
    }

    protected virtual void Move()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamageable target))
        {
            target.TakeDamage(damage);
        }
        ReturnToPool();
    }

    protected void ReturnToPool()
    {
        ProjectilePool.Instance.ReturnToPool(projectileName, this);
    }
}
