using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : State
{
    MainData enemy;
    private NavMeshAgent _navMeshAgent;

    public virtual void StartState(MainData mainData)
    {
        enemy = GetComponent<Enemy>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    public virtual void ExecuteState()
    {
        Chase(GameManager.instance.PlayerRef.transform);
    }

    public virtual void EndState()
    {

    }

    void Awake()
    {

    }

    void Update()
    {
        /*switch (enemy.enemyState.currentState)
        {
            case EnemyState.EEnemyState.Idle:
                break;
            case EnemyState.EEnemyState.Chase:
                Chase(GameManager.instance.PlayerRef.transform);
                break;
            case EnemyState.EEnemyState.Attack:
                break;
            case EnemyState.EEnemyState.Patrol:
                Patrol();
                break;
            default:
                break;
        }*/
    }

    void Chase(Transform _transform)
    {
        if (_navMeshAgent.isOnNavMesh)
        {
            _navMeshAgent.SetDestination(_transform.position);
        }
    }

    void Patrol()
    {
        if (_navMeshAgent.isOnNavMesh)
        {
            if (PathComplet())
            {
                float walkRadius = 10;
                Vector3 randomDirection = Random.insideUnitSphere * walkRadius;
                randomDirection += transform.position;
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
