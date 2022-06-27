using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "States/Patroling")]
public class Patroling : AState
{
    public float walkRadius = 1000;
    AMovementEnemy _movementEnemy;

    public override void StartState(AMainData mainData)
    {
        base.StartState(mainData);
        _movementEnemy = mainData.GetComponent<AMovementEnemy>();
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
        if (_movementEnemy.NavMeshAgent.isOnNavMesh)
        {
            if (PathComplet())
            {
                Vector3 randomDirection = Random.insideUnitSphere * walkRadius;
                randomDirection += _mainData.transform.position;
                NavMeshHit hit;
                NavMesh.SamplePosition(randomDirection, out hit, walkRadius, 1);
                Vector3 finalPosition = hit.position;
                _movementEnemy.NavMeshAgent.SetDestination(finalPosition);
            }
        }
    }

    bool PathComplet()
    {
        if (Vector3.Distance(_movementEnemy.NavMeshAgent.destination, _movementEnemy.NavMeshAgent.transform.position) <= _movementEnemy.NavMeshAgent.stoppingDistance)
        {
            return true;
        }
        if (!_movementEnemy.NavMeshAgent.hasPath || _movementEnemy.NavMeshAgent.velocity.magnitude <= 0.1f)
        {
            return true;
        }
        return false;
    }
}
