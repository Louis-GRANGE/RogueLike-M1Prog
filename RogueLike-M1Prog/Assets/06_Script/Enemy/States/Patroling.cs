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
    }

    public override void ExecuteState()
    {
        Patrol();
    }

    public override void EndState()
    {

    }

    void Patrol()
    {
        if (_mainData.MovementManager.NavMeshAgent.isOnNavMesh)
        {
            if (PathComplet())
            {
                Vector3 randomDirection = Random.insideUnitSphere * walkRadius;
                randomDirection += _mainData.transform.position;
                NavMeshHit hit;
                NavMesh.SamplePosition(randomDirection, out hit, walkRadius, 1);
                Vector3 finalPosition = hit.position;
                _mainData.MovementManager.NavMeshAgent.SetDestination(finalPosition);
            }
        }
    }

    bool PathComplet()
    {
        if (Vector3.Distance(_mainData.MovementManager.NavMeshAgent.destination, _mainData.MovementManager.NavMeshAgent.transform.position) <= _mainData.MovementManager.NavMeshAgent.stoppingDistance)
        {
            return true;
        }
        if (!_mainData.MovementManager.NavMeshAgent.hasPath || _mainData.MovementManager.NavMeshAgent.velocity.magnitude <= 0.1f)
        {
            return true;
        }
        return false;
    }
}
