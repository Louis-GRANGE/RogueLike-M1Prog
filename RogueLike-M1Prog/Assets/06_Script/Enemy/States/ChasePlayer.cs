using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "States/ChasePlayer")]
public class ChasePlayer : AState
{
    Transform _playerTransform;
    AMovementEnemy _movementEnemy;

    public override void StartState(AMainData mainData)
    {
        base.StartState(mainData);
        _playerTransform = GameManager.instance.PlayerRef.transform;
        _movementEnemy = mainData.GetComponent<AMovementEnemy>();

    }

    public override void ExecuteState()
    {
        if (_playerTransform)
            Chase(_playerTransform);
    }

    public override void EndState()
    {

    }
    void Chase(Transform _transform)
    {
        if (_movementEnemy.NavMeshAgent.isOnNavMesh)
        {
            _movementEnemy.NavMeshAgent.SetDestination(_transform.position);
        }
    }
}
