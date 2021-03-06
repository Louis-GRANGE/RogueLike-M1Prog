using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : AMainData
{
    public SOEnemy SOEnemy;

    public Room RefInRoom;
    public int AIIndex;

    private void OnDestroy()
    {
        if (GameManager.instance && GameManager.instance.EnemyManagerRef)
            GameManager.instance.EnemyManagerRef.Enemies.Remove(this);

        if(RefInRoom)
            RefInRoom.SendAIDied(this);
    }

    void Start()
    {
        if (SOEnemy)
        {
            name = SOEnemy.EnemyName;
            HealthManager.maxHealth = SOEnemy.Health;
            HealthManager.health = SOEnemy.Health;
        }
        else
            name = "Enemy";
    }

    void Update()
    {

    }
}
