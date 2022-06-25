using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGWeapon : AWeapon
{
    PlayerWeaponManager playerWeaponManager;
    private void Start()
    {
        playerWeaponManager = ((PlayerWeaponManager)_owner.ownerMainData.WeaponManager);
    }

    public override bool Shoot(Vector3 shootDirection, float additionnalSpray = 0)
    {
        if (!base.Shoot(shootDirection, additionnalSpray))
            return false;

        if (weaponData.AmmoPrefab)
        {
            if (playerWeaponManager && playerWeaponManager.HaveTarget)
            {
                Vector3 targetDirection = playerWeaponManager.TargetShootPos - _canon.transform.position;
                Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, 360.0f, 0.0f);

                Instantiate(weaponData.AmmoPrefab, _canon.transform.position, Quaternion.LookRotation(newDirection));
            }
            else
                Instantiate(weaponData.AmmoPrefab, _canon.transform.position - Vector3.up * 0.1f, playerWeaponManager.transform.rotation);

            PlaySound();
        }
        return true;
    }
}
