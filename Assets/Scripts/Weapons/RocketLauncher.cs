using UnityEngine;

public class RocketLauncher : WeaponBase
{
    [SerializeField] private GameObject rocketPrefab;
    [SerializeField] private Transform firePoint;

    public override void Use()
    {
        if (Time.time < nextFireTime) return;

        if (currentAmmo <= 0)
        {
            ChangeState(WeaponState.Empty);
            return;
        }

        nextFireTime = Time.time + fireRate;
        currentAmmo--;

        ChangeState(WeaponState.Firing);
        NotifyAmmoChange();

        Debug.Log($"{weaponName} launched a rocket!");

        // TODO: instantiate rocket prefab later
        if (rocketPrefab && firePoint)
            Instantiate(rocketPrefab, firePoint.position, firePoint.rotation);
    }
}
