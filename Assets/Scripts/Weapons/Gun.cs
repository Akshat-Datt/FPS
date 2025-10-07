// Gun.cs
using UnityEngine;

[RequireComponent(typeof(AudioSource))] // optional, for click/sfx later
public class Gun : WeaponBase
{
    [Header("Gun Settings")]
    [SerializeField] private Camera fpsCamera = null;
    [SerializeField] private float range = 100f;
    [SerializeField] private int damage = 25;

    private void Reset()
    {
        // try to auto-assign if possible
        if (fpsCamera == null && Camera.main != null) fpsCamera = Camera.main;
    }

    public override void Use()
    {
        // Check common conditions
        if (!CanFire()) return;

        // Enforce cooldown
        nextFireTime = Time.time + fireRate;

        // consume ammo
        ConsumeAmmo(1);

        // Set state (briefly) — in a full impl you'd return to Idle after animation/shot
        ChangeState(WeaponState.Firing);

        // Visual/Audio feedback hooks (you can add muzzle particles, SFX)
        Debug.Log($"{WeaponName} fired. Ammo: {CurrentAmmo}/{MaxAmmo}");

        // Basic hitscan: detect damageable objects
        if (fpsCamera != null)
        {
            if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out RaycastHit hit, range))
            {
                var dmg = hit.collider.GetComponent<Damageable>();
                if (dmg != null)
                {
                    dmg.TakeDamage(damage);
                    Debug.Log($"Hit {hit.collider.name} for {damage}");
                }
            }
        }

        // If out of ammo after this shot, change to Empty (ConsumeAmmo already handles it)
        if (CurrentAmmo <= 0) ChangeState(WeaponState.Empty);
        else ChangeState(WeaponState.Idle);
    }
}
