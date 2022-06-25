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

    public int RunSeed = 0;

    public int Difficulty = 1;

    [HideInInspector]
    public SaveScriptableObject GameSave;

    protected override void Awake()
    {
        base.Awake();
        SetRunSeed(RunSeed);
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        GameSave.LoadSave();
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
            RunSeed = seed;
            Random.InitState(RunSeed + Difficulty);
        }
        else
        {
            RunSeed = (int)System.DateTime.Now.Ticks;
            Random.InitState(RunSeed + Difficulty);
        }
        Debug.Log("[GameManager] Seed: " + RunSeed + " Difficulty: " + Difficulty + " = " + (RunSeed + Difficulty));
    }


    public void StartGame()
    {
        Instantiate(PlayerPref);
        
        Player player = Player.Instance;

    }

    public void OnPlayerDied()
    {
        MenuManager.instance.looseMenu.ShowMenu();
    }

    public void Save(bool dead = false)
    {
        if (dead)
            GameSave.SetValues();
        else
        {
            Player player = Player.Instance;
            GameSave.SetValues(true, player.WeaponManager._weaponData, player.HealthManager.health, player.WeaponManager.weapon._munitions, Difficulty, RunSeed, player.playerStats.NumberKills, player.playerStats.DamagesDeals, player.playerStats.DamageTaked);
        }
    }
}
