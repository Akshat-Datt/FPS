using UnityEngine;

public class RocketProjectile : ProjectileBase
{
    [Header("Rocket Settings")]
    [SerializeField] private float explosionRadius = 5f;
    // [SerializeField] private int damage = 50;

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile") || other.CompareTag("Player")) return;

        Collider[] hits = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (var hit in hits)
        {
            if (hit.TryGetComponent(out IDamageable target))
            {
                target.TakeDamage(damage);
            }
        }

        ParticlePool.Instance.PlayExplosion(transform.position);

        ProjectilePool.Instance.ReturnToPool(name, this);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
