using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    public SOEnemy SOEnemy;
    private NavMeshAgent _navMeshAgent;

    public Room RefInRoom;
    public int AIIndex;

    private void OnDestroy()
    {
        GameManager.instance.EnemyManagerRef.Enemies.Remove(this);
        RefInRoom.SendAIDied(this);
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
