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
            explodeBomb.DamageSender = Sender;
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

        //explodeBomb.DamageSender = Sender;
        explodeBomb.Explode();
        SoundManager.Instance.RequestSoundEffect(transform.position, SoundType.Explosion);
        //Destroy(gameObject);
        return true;
    }

    public override bool OnDeath()
    {
        if (base.OnDeath())
            return true;

        //explodeBomb.DamageSender = gameObject;
        explodeBomb.Explode();
        SoundManager.Instance.RequestSoundEffect(transform.position, SoundType.Explosion);
        //Destroy(gameObject);
        return true;
    }
}
