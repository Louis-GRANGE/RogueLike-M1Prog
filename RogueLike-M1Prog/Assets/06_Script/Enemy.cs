using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class Enemy : MonoBehaviour
{
    public SOEnemy SOEnemy;
    private NavMeshAgent _navMeshAgent;

    private void OnDestroy()
    {
        GameManager.instance.EnemyManagerRef.Enemies.Remove(this);
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
