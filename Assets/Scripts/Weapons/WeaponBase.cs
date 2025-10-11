// WeaponBase.cs
using System;
using System.Collections;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private string weaponName = "Weapon";
    [SerializeField] private int maxAmmo = 10;
    [SerializeField] protected float fireRate = 0.2f;
    [SerializeField] private float reloadDuration = 1.2f;

    protected int currentAmmo;
    protected float nextFireTime = 0f;
    protected WeaponState currentState = WeaponState.Idle;

    // Read-only properties for other systems
    public string WeaponName => weaponName;
    public int MaxAmmo => maxAmmo;
    public int CurrentAmmo => currentAmmo;
    public WeaponState State => currentState;

    // Events (static to make listening easy from UI/managers)
    // signature: (weaponName, currentAmmo, maxAmmo)
    public static event Action<string,int,int> OnAmmoChanged;
    // signature: (weaponName, newState)
    public static event Action<string,WeaponState> OnWeaponStateChanged;

    protected virtual void Awake()
    {
        currentAmmo = maxAmmo;
        NotifyAmmo();
        ChangeState(WeaponState.Idle);
    }

    protected void ChangeState(WeaponState newState)
    {
        currentState = newState;
        OnWeaponStateChanged?.Invoke(weaponName, newState);
    }

    public void NotifyAmmo()
    {
        OnAmmoChanged?.Invoke(weaponName, currentAmmo, maxAmmo);
    }

    // Basic checks that derived weapons should use before firing
    protected bool CanFire()
    {
        if (currentState == WeaponState.Reloading) return false;
        if (Time.time < nextFireTime) return false;
        if (currentAmmo <= 0)
        {
            ChangeState(WeaponState.Empty);
            return false;
        }
        return true;
    }

    protected void ConsumeAmmo(int amount = 1)
    {
        currentAmmo = Mathf.Max(0, currentAmmo - amount);
        NotifyAmmo();
        if (currentAmmo <= 0) ChangeState(WeaponState.Empty);
    }

    // Public reload entry (can be called by input)
    public virtual void StartReload()
    {
        if (currentState == WeaponState.Reloading) return;
        StartCoroutine(ReloadRoutine());
    }

    private IEnumerator ReloadRoutine()
    {
        ChangeState(WeaponState.Reloading);
        yield return new WaitForSeconds(reloadDuration);
        currentAmmo = maxAmmo;
        NotifyAmmo();
        ChangeState(WeaponState.Idle);
    }

    public abstract void Use();
}
