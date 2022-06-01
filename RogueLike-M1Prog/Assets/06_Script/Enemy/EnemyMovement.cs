using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    Enemy enemy;
    private NavMeshAgent _navMeshAgent;

    void Awake()
    {
        enemy = GetComponent<Enemy>();
        enemy.enemyMovement = this;
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        enemy.enemyState.OnStateChange += OnStateChange;
    }

    void Update()
    {
        switch (enemy.enemyState.currentState)
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
        }
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

    // When State Change
    void OnStateChange(EnemyState.EEnemyState enemyState)
    {
        switch (enemyState)
        {
            case EnemyState.EEnemyState.Idle:
                break;
            case EnemyState.EEnemyState.Chase:
                break;
            case EnemyState.EEnemyState.Attack:
                break;
            default:
                break;
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
