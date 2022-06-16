using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : AMainData
{
    [HideInInspector] public static Player Instance;

    [HideInInspector] public PlayerMovement playerMovement;
    [HideInInspector] public PlayerShoot playerShoot;
    [HideInInspector] public PlayerInputs playerInputs;

    private void Awake()
    {
        if (!Instance)
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        playerMovement = GetComponent<PlayerMovement>();
        playerShoot = GetComponent<PlayerShoot>();
        HealthManager = GetComponent<PlayerHealth>();
    }

    private void Start()
    {
        GameManager.instance.PlayerRef = gameObject;
    }
}
