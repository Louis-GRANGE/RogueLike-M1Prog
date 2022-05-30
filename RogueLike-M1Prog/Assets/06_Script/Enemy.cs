using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    public SOEnemy SOEnemy;
    private NavMeshAgent _navMeshAgent;

    public UnityEvent onAIDeath;

    private void OnDestroy()
    {
        GameManager.instance.EnemyManagerRef.Enemies.Remove(this);
        onAIDeath.Invoke();
    }

    void Start()
    {
        name = SOEnemy.EnemyName;
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        _navMeshAgent.SetDestination(GameManager.instance.PlayerRef.transform.position);
    }
}
