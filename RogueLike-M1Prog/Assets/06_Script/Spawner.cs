using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public SOSpawner SOSpawner;
    private List<SOEnemy> SOEnemies;

    private void Start()
    {
        SOEnemies = SOSpawner.getFlatList();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            int index = Random.Range(0, SOEnemies.Count);
            Debug.Log(SOEnemies.Count);
            Enemy enemy = Instantiate(SOEnemies[index].Prefab, transform.position + Random.insideUnitSphere * 5, Quaternion.identity);
            enemy.SOEnemy = SOEnemies[index];
        }
    }
}