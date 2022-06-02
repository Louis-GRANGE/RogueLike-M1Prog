using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "States/Patroling")]
public class Patroling : AState
{
    public float walkRadius = 1000;

    public override void StartState(AMainData mainData)
    {
        base.StartState(mainData);
        Debug.Log("[INIT] Patroling");
    }

    public override void ExecuteState(AMainData mainData)
    {
        Patrol(mainData);
    }

    public override void EndState(AMainData mainData)
    {

    }

    void Patrol(AMainData mainData)
    {
        if (mainData.MovementManager.NavMeshAgent.isOnNavMesh)
        {
            if (PathComplet(mainData))
            {
                Vector3 randomDirection = Random.insideUnitSphere * walkRadius;
                randomDirection += mainData.transform.position;
                NavMeshHit hit;
                NavMesh.SamplePosition(randomDirection, out hit, walkRadius, 1);
                Vector3 finalPosition = hit.position;
                mainData.MovementManager.NavMeshAgent.SetDestination(finalPosition);
            }
        }
    }

    bool PathComplet(AMainData mainData)
    {
        if (Vector3.Distance(mainData.MovementManager.NavMeshAgent.destination, mainData.MovementManager.NavMeshAgent.transform.position) <= mainData.MovementManager.NavMeshAgent.stoppingDistance)
        {
            return true;
        }
        if (!mainData.MovementManager.NavMeshAgent.hasPath || mainData.MovementManager.NavMeshAgent.velocity.magnitude <= 0.1f)
        {
            return true;
        }
        return false;
    }
}
