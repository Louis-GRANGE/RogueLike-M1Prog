using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameObject PlayerRef;
    public MapGenerator MapGeneratorRef;
    public Spawner SpawnerRef;
    public EnemyManager EnemyManagerRef;

    public int Difficulty = 1;

    private void Update()
    {
        /*if(Input.GetKeyDown(KeyCode.Z))
        {
            NextLevel();
        }*/
    }

    public void NextLevel()
    {
        PlayerRef.transform.position = Vector3.up * 10000;
        Difficulty++;
        LevelManager.instance.LoadNewMap();
    }
}
