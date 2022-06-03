using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "States/ChasePlayer")]
public class ChasePlayer : AState
{
    Transform playerTransform;

    public override void StartState(AMainData mainData)
    {
        base.StartState(mainData);
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
        if (_mainData.MovementManager.NavMeshAgent.isOnNavMesh)
        {
            _mainData.MovementManager.NavMeshAgent.SetDestination(_transform.position);
        }
    }
}
