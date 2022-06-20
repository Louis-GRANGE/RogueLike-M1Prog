using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : Singleton<InputManager>
{
    [Header("External")]
    public PlayerInputManager playerInputManager;

    public InputDevice lastDevice;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        playerInputManager.onPlayerJoined += PlayerInputManager_onPlayerJoined;
        InputSystem.onActionChange += InputSystem_onActionChange;
    }

    private void OnDestroy()
    {
        playerInputManager.onPlayerJoined -= PlayerInputManager_onPlayerJoined;
        InputSystem.onActionChange -= InputSystem_onActionChange;
    }

    private void InputSystem_onActionChange(object obj, InputActionChange change)
    {
        if (change == InputActionChange.ActionPerformed)
            lastDevice = ((InputAction)obj).activeControl.device;
    }


    private void PlayerInputManager_onPlayerJoined(PlayerInput obj)
    {
        
    }
}
