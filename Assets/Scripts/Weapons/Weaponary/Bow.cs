using UnityEngine;

public class Bow : WeaponBase
{
    [Header("Projectile Settings")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject arrowPrefab;

    public override void Use()
    {
        if (!CanFire()) return;

        nextFireTime = Time.time + fireRate;
        ConsumeAmmo(1);
        ChangeState(WeaponState.Firing);

        if (arrowPrefab && firePoint)
        {
            GameObject arrow = Instantiate(arrowPrefab, firePoint.position, firePoint.rotation);
            Debug.Log($"{WeaponName} fired an arrow!");
        }

        ChangeState(WeaponState.Idle);
    }
}
