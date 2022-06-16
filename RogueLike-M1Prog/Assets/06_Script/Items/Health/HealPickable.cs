using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPickable : APickable
{
    HealItem item;

    private void Start()
    {
        item = transform.parent.GetComponent<HealItem>();
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.TagPlayer))
        {
            AMainData mainData;
            if (other.TryGetComponent(out mainData))
            {
                if (mainData.HealthManager && mainData.HealthManager.health < mainData.HealthManager.maxHealth)
                {
                    mainData.HealthManager.ModifyHealth(item.healthData.health);
                    DamageTextPool.instance.RequestHealText(transform.position, item.healthData.health);
                    item.Desactivate();
                }
            }
        }
    }
}