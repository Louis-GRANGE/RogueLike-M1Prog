using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : AState
{
    NavMeshAgent _navMeshAgent;

    public override void StartState(AMainData mainData)
    {
        base.StartState(mainData);
        _mainData = mainData.GetComponent<Enemy>();
        _navMeshAgent = mainData.GetComponent<NavMeshAgent>();
    }

    public override void ExecuteState()
    {
        Chase(GameManager.instance.PlayerRef.transform);
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
