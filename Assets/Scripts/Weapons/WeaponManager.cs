// WeaponManager.cs
using System;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private WeaponBase[] weapons;

    private int currentIndex = 0;

    // broadcasts currently equipped weapon name
    public static event Action<string> OnWeaponEquipped;

    private void Start()
    {
        if (weapons == null || weapons.Length == 0) return;
        Equip(0);
    }

    private void Update()
    {
        // quick number switching
        if (Input.GetKeyDown(KeyCode.Alpha1)) Equip(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) Equip(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) Equip(2);

        // firing input (hold for automatic; pressed is up to weapon implementation)
        if (Input.GetButton("Fire1"))
        {
            weapons[currentIndex]?.Use();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            weapons[currentIndex]?.StartReload();
        }
    }

    public void Equip(int index)
    {
        if (weapons == null || weapons.Length == 0) return;
        if (index < 0 || index >= weapons.Length) return;

        for (int i = 0; i < weapons.Length; i++)
            weapons[i].gameObject.SetActive(i == index);

        currentIndex = index;
        OnWeaponEquipped?.Invoke(weapons[currentIndex].WeaponName);

        // notify UI of current weapon's ammo so UI shows accurate info immediately
        weapons[currentIndex].NotifyAmmo();
    }

    public WeaponBase GetCurrentWeapon() => weapons != null && weapons.Length > 0 ? weapons[currentIndex] : null;
}
