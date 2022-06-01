using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    Enemy enemy;
    private NavMeshAgent _navMeshAgent;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
        enemy.enemyMovement = this;
        _navMeshAgent = GetComponent<NavMeshAgent>();

        enemy.enemyState.OnStateChange += OnStateChange;
    }

    private void Update()
    {
        switch (enemy.enemyState.currentState)
        {
            case EnemyState.EEnemyState.Idle:
                break;
            case EnemyState.EEnemyState.Chase:
            {
                Chase(GameManager.instance.PlayerRef.transform);
                break;
            } 
            case EnemyState.EEnemyState.Attack:
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
}
