using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketHit : MonoBehaviour
{
    [Header("External")]
    public AExplode Explosion;
    private Collider TriggerToExplode;

    private void Start()
    {
        transform.parent.GetComponent<Rigidbody>().AddForce(transform.forward * 50, ForceMode.Impulse);
        TriggerToExplode = GetComponent<Collider>();
        Destroy(transform.parent.gameObject, 5);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger)
            Physics.IgnoreCollision(other, TriggerToExplode);
        else
            Explosion.Explode();
    }

    private void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, 2, ~(1 << Constants.LayerItem), QueryTriggerInteraction.Ignore))
        {
            Explosion.Explode();
        }
    }
}
