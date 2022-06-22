using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketHit : MonoBehaviour
{
    [Header("External")]
    public AExplode Explosion;
    private void Awake()
    {
        transform.parent.GetComponent<Rigidbody>().AddForce(transform.right * 50, ForceMode.Impulse);
    }
    private void OnTriggerEnter(Collider other)
    {
        Explosion.Explode();
    }
}
