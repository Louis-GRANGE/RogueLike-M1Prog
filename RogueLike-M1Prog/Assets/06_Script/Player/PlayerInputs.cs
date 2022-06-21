using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum EDeviceType
{
    KeyboardAndMouse,
    Gamepad
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

        inputs.Enable();
    }

    private void Start()
    {
        Player.Instance.playerInputs = this;
        playerInput = GetComponent<PlayerInput>();
        
        //Setup Callbacks
        inputs.Player.Move.performed += Player.Instance.playerMovement.OnMove;
        inputs.Player.Move.canceled += Player.Instance.playerMovement.OnMove;

        inputs.Player.Look.performed += Player.Instance.playerMovement.OnLook;
        inputs.Player.Look.canceled += Player.Instance.playerMovement.OnLook;

        inputs.Player.Fire.performed += Player.Instance.playerShoot.Fire;
        inputs.Player.Fire.canceled += Player.Instance.playerShoot.Fire;

        inputs.Player.Interact.started += Player.Instance.playerShoot.Interact;
        inputs.Player.Interact.canceled += Player.Instance.playerShoot.UnInteract;

        playerInput.onControlsChanged += OnControlsChanged;
    }


    private void Update()
    {
        CurrentDeviceType = GetCurrentDeviceType();
    }

    private void OnDestroy()
    {
        playerInput.onControlsChanged -= OnControlsChanged;
        inputs.Player.Move.performed -= Player.Instance.playerMovement.OnMove;
        inputs.Player.Move.canceled -= Player.Instance.playerMovement.OnMove;

        inputs.Player.Look.performed -= Player.Instance.playerMovement.OnLook;
        inputs.Player.Look.canceled -= Player.Instance.playerMovement.OnLook;

        inputs.Player.Fire.performed -= Player.Instance.playerShoot.Fire;
        inputs.Player.Fire.canceled -= Player.Instance.playerShoot.Fire;

        inputs.Player.Interact.started -= Player.Instance.playerShoot.Interact;
        inputs.Player.Interact.canceled -= Player.Instance.playerShoot.UnInteract;
        inputs.Disable();
    }

    private void OnDisable()
    {
        playerInput.onControlsChanged -= OnControlsChanged;
        inputs.Player.Move.performed -= Player.Instance.playerMovement.OnMove;
        inputs.Player.Move.canceled -= Player.Instance.playerMovement.OnMove;

        inputs.Player.Look.performed -= Player.Instance.playerMovement.OnLook;
        inputs.Player.Look.canceled -= Player.Instance.playerMovement.OnLook;

        inputs.Player.Fire.performed -= Player.Instance.playerShoot.Fire;
        inputs.Player.Fire.canceled -= Player.Instance.playerShoot.Fire;

        inputs.Player.Interact.started -= Player.Instance.playerShoot.Interact;
        inputs.Player.Interact.canceled -= Player.Instance.playerShoot.UnInteract;
        inputs.Disable();
    }

    private void OnControlsChanged(UnityEngine.InputSystem.PlayerInput obj)
    {
        Debug.Log($"Control Scheme changed : {playerInput.currentControlScheme}");
        CurrentDeviceType = GetCurrentDeviceType();
        OnDeviceTypeChanged?.Invoke(CurrentDeviceType);
    }

    public EDeviceType GetCurrentDeviceType()
    {
        Debug.Log(playerInput.currentControlScheme);
        switch (playerInput.currentControlScheme)
        {
            case "KeyboardAndMouse":
                return EDeviceType.KeyboardAndMouse;
            case "Gamepad":
                return EDeviceType.Gamepad;
            default:
                return EDeviceType.Gamepad;
        }
    }
}
