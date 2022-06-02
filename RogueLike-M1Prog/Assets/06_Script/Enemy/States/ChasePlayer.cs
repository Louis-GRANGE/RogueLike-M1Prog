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
        Debug.Log("[INIT] ChasePlayer");
        playerTransform = GameManager.instance.PlayerRef.transform;
    }

    public override void ExecuteState(AMainData mainData)
    {
        Chase(mainData, playerTransform);
    }

    public override void EndState(AMainData mainData)
    {

    }
    void Chase(AMainData mainData, Transform _transform)
    {
        if (mainData.MovementManager.NavMeshAgent.isOnNavMesh)
        {
            mainData.MovementManager.NavMeshAgent.SetDestination(_transform.position);
        }
    }
}
