using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public abstract class APickable : MonoBehaviour
{
    protected AItem item;

    protected virtual void Start()
    {
        item = transform.parent.GetComponent<AItem>();
    }
    protected virtual void OnTriggerEnter(Collider other)
    {

    }
    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Constants.TagPlayer) && item)
            item.HideShown();
    }
}
