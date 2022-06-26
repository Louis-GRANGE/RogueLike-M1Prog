using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObjects/Enemy")]
public class SOEnemy : ScriptableObject
{
    public int Health;
    public string EnemyName;
    public Enemy Prefab;
    public Vector2Int SpawningRangeChipsMoney;
}
