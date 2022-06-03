using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public abstract class AMovementManager : MonoBehaviour
{
    protected AMainData ownerMainData;
    public NavMeshAgent NavMeshAgent;

    public virtual void Awake()
    {
        ownerMainData = GetComponent<AMainData>();
        ownerMainData.MovementManager = this;

        NavMeshAgent = ownerMainData.GetComponent<NavMeshAgent>();
    }
}
