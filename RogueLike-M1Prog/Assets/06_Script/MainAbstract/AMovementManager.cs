using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public abstract class AMovementManager : MonoBehaviour
{
    protected AMainData ownerMainData;
    public NavMeshAgent NavMeshAgent;

    private void Awake()
    {
        ownerMainData = GetComponent<AMainData>();
        ownerMainData.MovementManager = this;

        NavMeshAgent = ownerMainData.GetComponent<NavMeshAgent>();
    }
}
