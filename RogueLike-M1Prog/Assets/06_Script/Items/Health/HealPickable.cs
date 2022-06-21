using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPickable : APickable
{
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.TagPlayer))
        {
            AMainData mainData;
            if (other.TryGetComponent(out mainData))
            {
                if (mainData.HealthManager && mainData.HealthManager.health < mainData.HealthManager.maxHealth)
                {
                    mainData.HealthManager.ModifyHealth(((HealItem)item).healthData.health);
                    DamageTextPool.instance.RequestHealText(transform.position, ((HealItem)item).healthData.health);
                    SoundManager.Instance.RequestSoundEffect(transform.position, SoundType.Item);
                    item.Desactivate();
                }
            }
        }
    }
}