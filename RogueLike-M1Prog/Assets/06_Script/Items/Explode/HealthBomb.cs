using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBomb : AHealth
{
    [Header("External")]
    public AExplode ExplodePref;
    private GameObject LastSender;


    public override bool TakeDamage(int damage, GameObject Sender, DamageType damageTypeSend)
    {
        if(base.TakeDamage(damage, Sender, damageTypeSend))
        {
            LastSender = Sender;
            DamageTextPool.instance.RequestDamageText(transform.position, damage);
            return true;
        }
        return false;
    }

    public override bool OnDeath(GameObject Sender)
    {
        if (base.OnDeath(Sender))
            return true;

        LastSender = Sender;
        AExplode exp = Instantiate(ExplodePref, transform.position, transform.rotation);
        exp.transform.SetParent(transform);
        exp.DamageSender = LastSender;
        exp.GetComponent<AExplode>().ExplodeAndDestroyOwner(gameObject);

        SoundManager.Instance.RequestSoundEffect(transform.position, SoundType.Explosion);
        return true;
    }

    public override bool OnDeath()
    {
        if (base.OnDeath())
            return true;

        if (LastSender)
        {
            AExplode exp = Instantiate(ExplodePref, transform.position, transform.rotation);
            exp.transform.SetParent(transform);
            exp.DamageSender = LastSender;
            exp.GetComponent<AExplode>().ExplodeAndDestroyOwner(gameObject);
        }
        else
        {
            AExplode exp = Instantiate(ExplodePref, transform.position, transform.rotation);
            exp.transform.SetParent(transform);
            exp.GetComponent<AExplode>().ExplodeAndDestroyOwner(gameObject);
        }


        SoundManager.Instance.RequestSoundEffect(transform.position, SoundType.Explosion);
        return true;
    }
}
