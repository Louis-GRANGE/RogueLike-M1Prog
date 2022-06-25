using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickable : APickable
{
    bool CanInteract;
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.TagPlayer))
        {
            item.ActualizeShown();
            CanInteract = true;
            AMainData mainData;
            if (other.TryGetComponent(out mainData))
            {
                if (mainData.WeaponManager && mainData.WeaponManager._weaponData)
                {
                    if (mainData.WeaponManager._weaponData == ((WeaponItem)item).weaponData)
                    {
                        mainData.WeaponManager.AddAmmo(((WeaponItem)item).munitions);
                        item.Desactivate();
                    }
                }
            }
        }
    }

    protected override void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Constants.TagPlayer) && item)
        {
            CanInteract = false;
            item.HideShown();
        }
    }

    private void Update()
    {
        if (CanInteract)
            Player.Instance.playerWeaponManager.InteractTrigger(item);
    }
}
