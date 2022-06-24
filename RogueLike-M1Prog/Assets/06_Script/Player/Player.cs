using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : AMainData
{
    [HideInInspector] public static Player Instance;

    [HideInInspector] public PlayerMovement playerMovement;
    [HideInInspector] public PlayerWeaponManager playerWeaponManager;
    [HideInInspector] public PlayerInputs playerInputs;
    [HideInInspector] public PlayerStats playerStats;

    private void Awake()
    {
        if (!Instance)
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        playerMovement = GetComponent<PlayerMovement>();
        playerWeaponManager = GetComponent<PlayerWeaponManager>();
        HealthManager = GetComponent<PlayerHealth>();
        playerStats = GetComponent<PlayerStats>();
    }

    private void Start()
    {
        GameManager.instance.PlayerRef = gameObject;
    }
}
