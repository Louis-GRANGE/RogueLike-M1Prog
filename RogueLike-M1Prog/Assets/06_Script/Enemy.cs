using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public SOEnemy SOEnemy;
    void Start()
    {
        name = SOEnemy.EnemyName;
    }

    void Update()
    {
        
    }
}
