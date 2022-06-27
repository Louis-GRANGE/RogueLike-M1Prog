using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class AMovementEnemy : AMovementManager
{
    [HideInInspector]
    public NavMeshAgent NavMeshAgent;
    protected override void Awake()
    {
        base.Awake();
        NavMeshAgent = ownerMainData.GetComponent<NavMeshAgent>();
    }

    protected virtual void Update()
    {
        Anim();
    }

    public virtual void Anim()
    {

    }
}
