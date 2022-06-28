using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : Singleton<InputManager>
{
    [Header("External")]
    public PlayerInputManager playerInputManager;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        playerInputManager.EnableJoining();
        GameManager.instance.onStartingGame += OnStartingGame;
    }

    private void OnDestroy()
    {
        GameManager.instance.onStartingGame -= OnStartingGame;
    }

    public void OnStartingGame()
    {
        playerInputManager.DisableJoining();
    }

    public void OnPlayerJoin(PlayerInput context)
    {
        GameManager.instance.Players.Add(context.gameObject.GetComponent<Player>());
        context.DeactivateInput();
        Transform spawnPoint = LobbyManager.instance.getSpawnPointAndRemoveIt();
        context.transform.position = spawnPoint.position;
        context.transform.rotation = spawnPoint.rotation;
        Debug.Log("Player Join Game: " + context.gameObject.name);
    }
}
