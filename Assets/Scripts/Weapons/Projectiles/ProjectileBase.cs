using UnityEngine;

public abstract class ProjectileBase : MonoBehaviour
{
    [SerializeField] protected float speed = 20f;
    [SerializeField] protected float lifeTime = 3f;
    [SerializeField] protected int damage = 25;

    protected float lifeTimer;
    protected bool isActive = true;

    protected virtual void OnEnable()
    {
        lifeTimer = 0f;
        isActive = true;
    }

    protected virtual void Update()
    {
        if (!isActive) return;

        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        lifeTimer += Time.deltaTime;
        if (lifeTimer >= lifeTime)
        {
            Deactivate();
        }
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (!isActive) return;

        var dmg = other.GetComponent<Damageable>();
        if (dmg != null)
        {
            dmg.TakeDamage(damage);
        }

        OnHit(other);
    }

    protected abstract void OnHit(Collider hit);

    protected virtual void Deactivate()
    {
        isActive = false;
        gameObject.SetActive(false);
    }
}
