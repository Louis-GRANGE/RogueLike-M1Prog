using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickable : APickable
{
    AmmoItem item;

    private void Start()
    {
        item = transform.parent.GetComponent<AmmoItem>();
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.TagPlayer))
        {
            AMainData mainData;
            if (other.TryGetComponent(out mainData))
            {
                if (mainData.WeaponManager)
                {
                    mainData.WeaponManager.AddAmmo(item.ammoData.ammo);
                    mainData.HealthManager.damageTextPool.RequestMunitionText(transform.position, item.ammoData.ammo);
                    item.Desactivate();
                }
            }
        }
    }
}