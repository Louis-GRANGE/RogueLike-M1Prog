using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBomb : AHealth
{
    [Header("External")]
    public ExplodeBomb explodeBomb;
    public override void TakeDamage(int damage, GameObject Sender)
    {
        base.TakeDamage(damage, Sender);



        damageTextPool.RequestDamageText(transform.position, damage);
        
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
