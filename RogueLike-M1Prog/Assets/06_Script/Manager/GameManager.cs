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
}
