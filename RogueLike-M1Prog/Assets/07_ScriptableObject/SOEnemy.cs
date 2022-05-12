using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObjects/Enemy")]
public class SOEnemy : ScriptableObject
{
    public float life;
    public string EnemyName;
    public Enemy Prefab;
}
