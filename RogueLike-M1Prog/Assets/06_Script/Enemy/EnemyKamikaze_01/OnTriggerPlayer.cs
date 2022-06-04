using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerPlayer : AExplode
{
    private AMainData ownerMainData;
    public GameObject Character;

    protected override void Start()
    {
        base.Start();
        ownerMainData = transform.parent.GetComponent<AMainData>();
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        if (other.CompareTag(Constants.TagPlayer))
        {
            ownerMainData.stateManager.ClearStates();
            Explode();
        }
    }
}
