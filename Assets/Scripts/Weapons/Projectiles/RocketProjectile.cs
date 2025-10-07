using UnityEngine;

public class RocketProjectile : ProjectileBase
{
    [Header("Explosion Settings")]
    [SerializeField] private float explosionRadius = 5f;

    protected override void OnHit(Collider hit)
    {
        // Simple explosion logic (will be replaced by pooled particles later)
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (var c in colliders)
        {
            var dmg = c.GetComponent<Damageable>();
            if (dmg != null)
            {
                dmg.TakeDamage(damage);
            }
        }

        Debug.Log($"Rocket exploded at {transform.position}");
        Deactivate();
    }
}
