using UnityEngine;

public class Sword : WeaponBase
{
    public override void Use()
    {
        ChangeState(WeaponState.Firing);
        Debug.Log($"{WeaponName} slashed!");
    }
}
