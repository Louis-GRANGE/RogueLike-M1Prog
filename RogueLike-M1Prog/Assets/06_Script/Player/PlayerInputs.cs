using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum EDeviceType
{
    KeyboardAndMouse,
    Gamepad,
    None
}

public class PlayerInputs : MonoBehaviour
{
    public PlayerActionss inputs;
    public PlayerInput playerInput;
    public EDeviceType CurrentDeviceType;
    public System.Action<EDeviceType> OnDeviceTypeChanged = null;

    private void Awake()
    {
        inputs = new PlayerActionss();
        playerInput = GetComponent<PlayerInput>();
    }

    private void Start()
    {
        CurrentDeviceType = GetCurrentDeviceType();

        GameManager.instance.onStartingGame += OnStartingGame;
    }

    private void OnDestroy()
    {
        GameManager.instance.onStartingGame -= OnStartingGame;
    }

    public EDeviceType GetCurrentDeviceType()
    {
        switch (playerInput.currentControlScheme)
        {
            case "KeyboardAndMouse":
                return EDeviceType.KeyboardAndMouse;
            case "Gamepad":
                return EDeviceType.Gamepad;
            default:
                return EDeviceType.None;
        }
    }

    public void OnStartingGame()
    {
        playerInput.ActivateInput();
        GetComponent<PlayerMovement>().CanMove = true;
    }
}
