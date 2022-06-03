using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public abstract class APickable : MonoBehaviour
{
    protected virtual void OnTriggerEnter(Collider other)
    {

    }
}
