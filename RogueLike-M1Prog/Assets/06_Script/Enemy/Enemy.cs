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
        if (GameManager.instance.EnemyManagerRef)
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
