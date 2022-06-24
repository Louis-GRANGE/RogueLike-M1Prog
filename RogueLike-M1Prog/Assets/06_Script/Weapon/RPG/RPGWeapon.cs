using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGWeapon : AWeapon
{
    public override bool Shoot(Vector3 shootDirection, float additionnalSpray = 0)
    {
        if (!base.Shoot(shootDirection, additionnalSpray))
            return false;

        if (_weaponData.AmmoPrefab)
        {
            Instantiate(_weaponData.AmmoPrefab, _canon.transform.position, _canon.transform.rotation);

            PlaySound();
        }
        return true;
    }
}
