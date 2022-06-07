//using GD.MinMaxSlider;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spawner", menuName = "ScriptableObjects/Spawner")]
public class SOSpawner : ScriptableObject
{
    //[MinMaxSlider(0, 100)]
    public Vector2Int EnnemisToSpawnRange;
    public List<EnemyProbability> enemyProbability;

    public List<SOEnemy> getFlatList()
    {
        List<SOEnemy> arrayEnemys = new List<SOEnemy>();

        foreach (EnemyProbability item in enemyProbability)
        {
            arrayEnemys.Add(item.SOEnemy);
        }

        return arrayEnemys;
    }

    public int GetRandomNbOfEnnemies()
    {
        return Random.Range(EnnemisToSpawnRange.x, EnnemisToSpawnRange.y);
    }
}

[System.Serializable]
public class EnemyProbability
{
    public SOEnemy SOEnemy;
    public int probability;
}
