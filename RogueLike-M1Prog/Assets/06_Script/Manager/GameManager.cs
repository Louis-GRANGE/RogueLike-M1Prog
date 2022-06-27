using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [Header("External")]
    public GameObject ChipsPerf;
    public GameObject PlayerPref;


    [HideInInspector]
    public GameObject PlayerRef;
    [HideInInspector]
    public Spawner SpawnerRef;
    [HideInInspector]
    public EnemyManager EnemyManagerRef;

    public int RunSeed = 0;

    public int Difficulty = 1;

    protected override void Awake()
    {
        base.Awake();
        SetRunSeed(RunSeed);
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        Debug.Log("Can Continue: " + SaveManager.instance.GetSave<SOSaveGame>().CanContinue);
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

        SetRunSeed(RunSeed);

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
        Debug.Log("PLAYER DIED");
        MenuManager.instance.looseMenu.ShowMenu();
    }

    public void Save(bool dead = false)
    {
        if (dead)
            SaveManager.instance.GetSave<SOSaveGame>().SetValues(false, null, 0, 0, 0, 0, 0, 0, 0);
        else
        {
            Debug.Log("BEFORE SET VALUE TO TRUE");
            Player player = Player.Instance;
            SaveManager.instance.GetSave<SOSaveGame>().SetValues(true, player.WeaponManager._weaponData, player.HealthManager.health, player.WeaponManager.weapon._munitions, Difficulty, RunSeed, player.playerStats.NumberKills, player.playerStats.DamagesDeals, player.playerStats.DamageTaked);
        }

        SaveManager.instance.SaveAll();
    }

    public void SpawnChips(Vector3 pos, int number)
    {
        for (int i = 0; i < number; i++)
        {
            Vector3 randomDirection = Random.insideUnitSphere * 2;
            randomDirection += pos;
            UnityEngine.AI.NavMeshHit hit;
            UnityEngine.AI.NavMesh.SamplePosition(randomDirection, out hit, 2, 1);
            Vector3 finalPosition = hit.position;
            if (finalPosition.x < Mathf.Infinity)
                Instantiate(ChipsPerf, finalPosition, Quaternion.identity); 
        }
    }
}
