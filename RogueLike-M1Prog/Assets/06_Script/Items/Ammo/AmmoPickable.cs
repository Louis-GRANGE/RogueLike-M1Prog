using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickable : APickable
{
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.TagPlayer))
        {
            AMainData mainData;
            if (other.TryGetComponent(out mainData))
            {
                if (mainData.WeaponManager)
                {
                    mainData.WeaponManager.AddAmmo(((AmmoItem)item).ammoData.ammo);
                    DamageTextPool.instance.RequestMunitionText(transform.position, ((AmmoItem)item).ammoData.ammo);
                    item.Desactivate();
                }
            }
        }
    }
}