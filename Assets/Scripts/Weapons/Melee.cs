using UnityEngine;

public class Sword : WeaponBase
{
    public override void Use()
    {
        ChangeState(WeaponState.Firing);
        Debug.Log($"{weaponName} slashed!");
        // TODO: add melee animation + hitbox check later
    }
}
