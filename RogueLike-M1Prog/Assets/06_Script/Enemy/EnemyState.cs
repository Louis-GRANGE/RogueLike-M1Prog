using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour
{
    Enemy enemy;
    public enum EEnemyState
    {
        Idle,
        Chase,
        Attack,
        Patrol
    }

    [SerializeField]
    private EEnemyState _currentState = EEnemyState.Chase;

    public EEnemyState currentState
    {
        get { return _currentState; }
        set
        {
            if (_currentState == value) return;
            _currentState = value;
            OnStateChange?.Invoke(_currentState);
        }
    }
    public System.Action<EEnemyState> OnStateChange;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
        enemy.enemyState = this;
        _currentState = EEnemyState.Patrol;
    }

    void Update()
    {
        switch (_currentState)
        {
             case EEnemyState.Idle:
                 break;
             case EEnemyState.Chase:
                 break;
             case EEnemyState.Attack:
                 break;
             default:
                 break;
        }
    }
}
