using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    public PlayerActionss inputs;

    private void Awake()
    {
        inputs = new PlayerActionss();
        inputs.Enable();
    }

    private void Start()
    {
        Player.Instance.playerInputs = this;

        //Setup Callbacks
        inputs.Player.Move.performed += Player.Instance.playerMovement.OnMove;
        inputs.Player.Look.performed += Player.Instance.playerMovement.OnLook;
        inputs.Player.Fire.performed += Player.Instance.playerShoot.Fire;
        inputs.Player.Interact.performed += Player.Instance.playerShoot.Interact;
    }

    private void OnDestroy()
    {
        inputs.Player.Move.performed -= Player.Instance.playerMovement.OnMove;
        inputs.Player.Look.performed -= Player.Instance.playerMovement.OnLook;
        inputs.Player.Fire.performed -= Player.Instance.playerShoot.Fire;
        inputs.Player.Interact.performed -= Player.Instance.playerShoot.Interact;
        inputs.Disable();
    }

    private void OnDisable()
    {
        inputs.Player.Move.performed -= Player.Instance.playerMovement.OnMove;
        inputs.Player.Look.performed -= Player.Instance.playerMovement.OnLook;
        inputs.Player.Fire.performed -= Player.Instance.playerShoot.Fire;
        inputs.Player.Interact.performed -= Player.Instance.playerShoot.Interact;
        inputs.Disable();
    }
}
