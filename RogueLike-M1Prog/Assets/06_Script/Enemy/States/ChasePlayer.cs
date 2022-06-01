using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChasePlayer : AState
{
    NavMeshAgent _navMeshAgent;
    Transform playerTransform;

    public override void StartState(AMainData mainData)
    {
        base.StartState(mainData);
        Debug.Log("[INIT] ChasePlayer");
        _navMeshAgent = mainData.gameObject.GetComponent<NavMeshAgent>();
        playerTransform = GameManager.instance.PlayerRef.transform;
    }

    public override void ExecuteState()
    {
        Chase(playerTransform);
    }

    public override void EndState()
    {

    }
    void Chase(Transform _transform)
    {
        if (_navMeshAgent.isOnNavMesh)
        {
            _navMeshAgent.SetDestination(_transform.position);
        }
    }
}
