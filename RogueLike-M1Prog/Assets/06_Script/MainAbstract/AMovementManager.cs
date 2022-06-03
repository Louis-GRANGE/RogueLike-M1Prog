using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public abstract class AMovementManager : MonoBehaviour
{
    protected AMainData ownerMainData;
    public NavMeshAgent NavMeshAgent;

    [Header("Internal Components")]
    protected Animator _animator;

    public virtual void Awake()
    {
        ownerMainData = GetComponent<AMainData>();
        ownerMainData.MovementManager = this;

        NavMeshAgent = ownerMainData.GetComponent<NavMeshAgent>();
        _animator = transform.GetChild(0).GetComponent<Animator>();
    }
}
