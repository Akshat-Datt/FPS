using UnityEngine;

public class Gun : WeaponBase
{
    [SerializeField] private float range = 100f;

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

        Debug.Log($"{weaponName} fired a bullet!");

        // TODO: add raycast hit logic later
    }
}
