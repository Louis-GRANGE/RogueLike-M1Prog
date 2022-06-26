using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spawner : MonoBehaviour
{
    public SOSpawner SOSpawner;
    private List<SOEnemy> SOEnemies;

    private void Start()
    {
        GameManager.instance.SpawnerRef = this;
        SOEnemies = SOSpawner.getFlatList();
    }

    public Enemy SpawnRandomAIOnPos(Vector3 pos)
    {
        int index = Random.Range(0, SOEnemies.Count);
        Enemy enemy = Instantiate(SOEnemies[index].Prefab, pos, Quaternion.identity);
        GameManager.instance.EnemyManagerRef.Enemies.Add(enemy);
        enemy.SOEnemy = SOEnemies[index];
        return enemy;
    }

    public void SpawnAll()
    {
        int randomRangeSpawnEntities = Random.Range(SOSpawner.EnnemisToSpawnRange.x, SOSpawner.EnnemisToSpawnRange.y);
        for (int i = 0; i < randomRangeSpawnEntities; i++)
        {
            int index = Random.Range(0, SOEnemies.Count);
            Enemy enemy = Instantiate(SOEnemies[index].Prefab, transform.position/* + Random.insideUnitSphere * 5*/, Quaternion.identity);
            enemy.SOEnemy = SOEnemies[index];
        }
    }
}