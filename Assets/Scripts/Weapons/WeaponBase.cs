using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    [Header("General Settings")]
    [SerializeField] protected string weaponName = "Weapon";
    [SerializeField] protected int maxAmmo = 10;
    [SerializeField] protected float fireRate = 0.5f;

    protected int currentAmmo;
    protected float nextFireTime;

    // State machine
    public enum WeaponState { Idle, Firing, Reloading, Empty }
    protected WeaponState currentState = WeaponState.Idle;

    // Events for ammo + state change
    public delegate void AmmoChanged(string weaponName, int currentAmmo, int maxAmmo);
    public static event AmmoChanged OnAmmoChanged;

    public delegate void WeaponStateChanged(string weaponName, WeaponState state);
    public static event WeaponStateChanged OnWeaponStateChanged;

    protected virtual void Start()
    {
        currentAmmo = maxAmmo;
        NotifyAmmoChange();
    }

    public abstract void Use(); // override in child

    public virtual void Reload()
    {
        currentAmmo = maxAmmo;
        ChangeState(WeaponState.Reloading);
        NotifyAmmoChange();
    }

    protected void ChangeState(WeaponState newState)
    {
        currentState = newState;
        OnWeaponStateChanged?.Invoke(weaponName, newState);
    }

    protected void NotifyAmmoChange()
    {
        OnAmmoChanged?.Invoke(weaponName, currentAmmo, maxAmmo);
    }
}
