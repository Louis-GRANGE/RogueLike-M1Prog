using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MainData
{
    public SOEnemy SOEnemy;
    //public EnemyState enemyState;
    public EnemyMovement enemyMovement;
    public EnemyAttack enemyAttack;

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
        
    }

    void Update()
    {

    }
}
