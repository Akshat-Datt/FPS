// UIManager.cs
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("HUD references")]
    [SerializeField] private TextMeshProUGUI weaponNameText = null;
    [SerializeField] private TextMeshProUGUI ammoText = null;

    private string currentWeaponName = "";

    private void OnEnable()
    {
        WeaponManager.OnWeaponEquipped += HandleWeaponEquipped;
        WeaponBase.OnAmmoChanged += HandleAmmoChanged;
    }

    private void OnDisable()
    {
        WeaponManager.OnWeaponEquipped -= HandleWeaponEquipped;
        WeaponBase.OnAmmoChanged -= HandleAmmoChanged;
    }

    private void HandleWeaponEquipped(string weaponName)
    {
        currentWeaponName = weaponName;
        if (weaponNameText != null) weaponNameText.text = weaponName;
    }

    private void HandleAmmoChanged(string weaponName, int current, int max)
    {
        if (weaponName != currentWeaponName) return;
        if (ammoText != null) ammoText.text = $"{current} / {max}";
    }
}
