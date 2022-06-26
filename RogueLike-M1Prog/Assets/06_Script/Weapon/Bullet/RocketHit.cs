using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketHit : ABullet
{
    [Header("External")]
    public AExplode ExplodePref;
    private Collider TriggerToExplode;

    private void Start()
    {
        transform.GetComponent<Rigidbody>().AddForce(transform.forward * 50, ForceMode.Impulse);
        TriggerToExplode = GetComponent<Collider>();
        Destroy(transform.gameObject, 5);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger)
            Physics.IgnoreCollision(other, TriggerToExplode);
        else
        {
            AExplode exp = Instantiate(ExplodePref, transform.position, transform.rotation);
            exp.DamageSender= Sender;
            exp.GetComponent<AExplode>().Explode();

            Destroy(gameObject);
        }
    }
}
