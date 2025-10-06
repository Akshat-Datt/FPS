using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private WeaponBase[] weapons;
    private int currentWeaponIndex = 0;

    private void Start()
    {
        EquipWeapon(0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) EquipWeapon(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) EquipWeapon(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) EquipWeapon(2);

        if (Input.GetButtonDown("Fire1")) weapons[currentWeaponIndex].Use();
        if (Input.GetKeyDown(KeyCode.R)) weapons[currentWeaponIndex].Reload();
    }

    private void EquipWeapon(int index)
    {
        if (index < 0 || index >= weapons.Length) return;

        for (int i = 0; i < weapons.Length; i++)
            weapons[i].gameObject.SetActive(i == index);

        currentWeaponIndex = index;
        Debug.Log($"Equipped {weapons[currentWeaponIndex].name}");
    }
}
