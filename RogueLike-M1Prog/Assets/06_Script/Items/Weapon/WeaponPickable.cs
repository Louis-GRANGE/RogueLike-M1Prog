using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickable : APickable
{
    WeaponItem item;

    private void Start()
    {
        item = transform.parent.GetComponent<WeaponItem>();
    }
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.TagPlayer))
        {
            AMainData mainData;
            Debug.Log("TRIGGER MUNITION");
            if (other.TryGetComponent(out mainData))
            {
                if (mainData.WeaponManager && mainData.WeaponManager._weaponData)
                {
                    if (mainData.WeaponManager._weaponData == item.weaponData)
                    {
                        mainData.WeaponManager.AddAmmo(item.munitions);
                        mainData.HealthManager.damageTextPool.RequestMunitionText(transform.position, item.munitions);
                        item.Desactivate();
                    }
                }
            }
        }
    }
}
