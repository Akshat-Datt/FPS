using UnityEngine;

public class ArrowProjectile : ProjectileBase
{
    [Header("Piercing Settings")]
    [SerializeField] private int maxTargets = 2;
    private int targetsHit = 0;

    protected override void OnHit(Collider hit)
    {
        targetsHit++;
        Debug.Log($"Arrow hit {hit.name}");
        if (targetsHit >= maxTargets)
        {
            Deactivate();
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        targetsHit = 0;
    }
}
