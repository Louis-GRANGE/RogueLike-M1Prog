using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketHit : MonoBehaviour
{
    [Header("External")]
    public AExplode Explosion;
    private Collider TriggerToExplode;
    
    private void Awake()
    {
        transform.parent.GetComponent<Rigidbody>().AddForce(transform.forward * 50, ForceMode.Impulse);
        TriggerToExplode = GetComponent<Collider>();
    }

    private void Start()
    {
        Destroy(transform.parent.gameObject, 5);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger)
            Physics.IgnoreCollision(other, TriggerToExplode);
        else
            Explosion.Explode();
    }
}
