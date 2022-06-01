using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    Enemy enemy;
    
    void Awake()
    {
        enemy = GetComponent<Enemy>();
        enemy.enemyAttack = this;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
