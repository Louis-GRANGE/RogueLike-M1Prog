using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public GameObject PlayerPref;
    [HideInInspector]
    public GameObject PlayerRef;
    [HideInInspector]
    public Spawner SpawnerRef;
    [HideInInspector]
    public EnemyManager EnemyManagerRef;

    public int RunSeed;

    public int Difficulty = 1;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        SetRunSeed(RunSeed);
    }

    private void Update()
    {
        /*if(Input.GetKeyDown(KeyCode.R))
        {
            //NextLevel();
            SetRunSeed(0);
        }*/
    }

    public void NextLevel()
    {
        PlayerRef.transform.position = Vector3.up * 10000;
        Difficulty++;
        SceneManager.LoadSceneAsync(Constants.GameLevel);

        SetRunSeed(RunSeed);
        //LevelManager.instance.LoadNewMap();
    }

    public void SetRunSeed(int seed)
    {
        if (seed != 0)
        {
            Random.InitState(seed + Difficulty);
            RunSeed = seed;
        }
        else
        {
            RunSeed = (int)System.DateTime.Now.Ticks;
            Random.InitState(RunSeed + Difficulty);
        }
        Debug.Log("[GameManager] Seed: " + RunSeed);
    }


    public void StartGame()
    {
        Instantiate(PlayerPref);
    }

    public void OnPlayerDied()
    {
        MenuManager.instance.looseMenu.ShowMenu();
    }
}
