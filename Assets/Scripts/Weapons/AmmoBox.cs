using UnityEngine;

public class AmmoBox : MonoBehaviour
{
    [Header("Ammo Box Settings")]
    [Tooltip("How much ammo to give depending on weapon type")]
    [SerializeField] private int ammoForGun = 20;
    [SerializeField] private int ammoForRocket = 3;
    [SerializeField] private int ammoForBow = 5;

    [Header("Pickup Settings")]
    [SerializeField] private float rotateSpeed = 45f;
    [SerializeField] private AudioClip pickupSound;

    private void Update()
    {
        // simple spinning animation
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        WeaponManager weaponManager = other.GetComponentInChildren<WeaponManager>();
        if (weaponManager == null)
        {
            Debug.LogWarning("AmmoBox: No WeaponManager found on Player.");
            return;
        }

        WeaponBase currentWeapon = weaponManager.GetCurrentWeapon();
        if (currentWeapon == null)
        {
            Debug.LogWarning("AmmoBox: Player has no active weapon.");
            return;
        }

        int ammoToAdd = GetAmmoAmountForWeapon(currentWeapon);

        // Reflect ammo change in WeaponBase
        AddAmmoToWeapon(currentWeapon, ammoToAdd);

        // Play sound
        if (pickupSound != null)
            AudioSource.PlayClipAtPoint(pickupSound, transform.position);

        // deactivate (or pool later)
        gameObject.SetActive(false);
    }

    private int GetAmmoAmountForWeapon(WeaponBase weapon)
    {
        string name = weapon.WeaponName.ToLower();

        if (name.Contains("rocket")) return ammoForRocket;
        if (name.Contains("bow")) return ammoForBow;
        return ammoForGun; // default
    }

    private void AddAmmoToWeapon(WeaponBase weapon, int amount)
    {
        // Increase current ammo, but don’t exceed max
        var newAmmo = Mathf.Min(weapon.CurrentAmmo + amount, weapon.MaxAmmo);

        // Use reflection of WeaponBase’s NotifyAmmo()
        // Since currentAmmo is protected, we’ll add this safely via a method
        var weaponType = weapon.GetType();
        var field = typeof(WeaponBase).GetField("currentAmmo", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        if (field != null)
        {
            field.SetValue(weapon, newAmmo);
            weapon.NotifyAmmo();
        }
    }
}
