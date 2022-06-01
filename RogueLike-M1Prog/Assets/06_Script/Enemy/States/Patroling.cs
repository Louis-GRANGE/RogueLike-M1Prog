using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patroling : AState
{
    NavMeshAgent _navMeshAgent;
    
    public override void StartState(AMainData mainData)
    {
        base.StartState(mainData);
        Debug.Log("[INIT] Patroling");
        _navMeshAgent = mainData.gameObject.GetComponent<NavMeshAgent>();
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
        if (_navMeshAgent.isOnNavMesh)
        {
            if (PathComplet())
            {
                float walkRadius = 10;
                Vector3 randomDirection = Random.insideUnitSphere * walkRadius;
                randomDirection += _mainData.transform.position;
                NavMeshHit hit;
                NavMesh.SamplePosition(randomDirection, out hit, walkRadius, 1);
                Vector3 finalPosition = hit.position;
                _navMeshAgent.SetDestination(finalPosition);
            }
        }
    }

    bool PathComplet()
    {
        if (Vector3.Distance(_navMeshAgent.destination, _navMeshAgent.transform.position) <= _navMeshAgent.stoppingDistance)
        {
            return true;
        }
        if (!_navMeshAgent.hasPath || _navMeshAgent.velocity.magnitude <= 0.1f)
        {
            return true;
        }
        return false;
    }
}
