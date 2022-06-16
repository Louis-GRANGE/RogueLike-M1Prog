using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBomb : AHealth
{
    [Header("External")]
    public ExplodeBomb explodeBomb;
    public override bool TakeDamage(int damage, GameObject Sender, DamageType damageTypeSend)
    {
        if(base.TakeDamage(damage, Sender, damageTypeSend))
        {
            DamageTextPool.instance.RequestDamageText(transform.position, damage);
            return true;
        }
        return false;
    }

    private void Update()
    {

    }

    public override bool OnDeath(GameObject Sender)
    {
        if (base.OnDeath(Sender))
            return true;

        explodeBomb.Explode();
        //Destroy(gameObject);
        return true;
    }

    public override bool OnDeath()
    {
        if (base.OnDeath())
            return true;


        explodeBomb.Explode();
        //Destroy(gameObject);
        return true;
    }
}
