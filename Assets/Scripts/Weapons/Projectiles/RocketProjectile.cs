using UnityEngine;

public class RocketProjectile : ProjectileBase
{
    [Header("Rocket Settings")]
    [SerializeField] private float explosionRadius = 5f;
    // [SerializeField] private int damage = 50;

    protected override void OnTriggerEnter(Collider other)
    {
        // Prevent hitting the shooter or itself
        if (other.CompareTag("Projectile") || other.CompareTag("Player")) return;

        // Apply area damage
        Collider[] hits = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (var hit in hits)
        {
            if (hit.TryGetComponent(out IDamageable target))
            {
                target.TakeDamage(damage);
            }
        }

        // Spawn pooled explosion effect
        ParticlePool.Instance.PlayExplosion(transform.position);

        // Return rocket to pool instead of destroying
        ProjectilePool.Instance.ReturnToPool(name, this);
    }

    private void OnDrawGizmosSelected()
    {
        // Visualize explosion radius in editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
