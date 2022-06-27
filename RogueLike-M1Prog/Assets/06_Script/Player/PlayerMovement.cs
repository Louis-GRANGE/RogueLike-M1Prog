using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : AMovementManager
{
    Player _player;

    //Movement
    [HideInInspector] public Vector2 moveInput;
    [HideInInspector] public Vector2 lookInput;
    [HideInInspector] public RaycastHit hitUnderMouse;

    [Header("External References")]
    Camera _mainCamera;

    [Header("Components")]
    Rigidbody _rigidbody;

    [Header("Metrics")]
    public float speed;

    public Vector3 offsetShoot;


    protected override void Awake()
    {
        base.Awake();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _player = GetComponent<Player>();
        _mainCamera = Camera.main;
    }

    public void OnMove(InputAction.CallbackContext context) { moveInput = context.ReadValue<Vector2>(); }
    public void OnLook(InputAction.CallbackContext context) { lookInput = context.ReadValue<Vector2>(); }


    private void FixedUpdate()
    {
        if (!_mainCamera)
            _mainCamera = Camera.main;

        switch (_player.playerInputs.CurrentDeviceType)
        {
            case EDeviceType.KeyboardAndMouse:
            {
                MouseLookDirection();
                break;
            }
            case EDeviceType.Gamepad:
            {
                transform.LookAt(new Vector3(transform.position.x + lookInput.x, transform.position.y, transform.position.z + lookInput.y));
                break;
            }
        }
        Movement();
    }

    void MouseLookDirection()
    {
        Vector3 pointDirection = _mainCamera.ScreenToWorldPoint(lookInput);

        if (Physics.Raycast(pointDirection, _mainCamera.transform.forward, out hitUnderMouse, 1000, ~(1 << Constants.LayerIgnoreMouseHit), QueryTriggerInteraction.Ignore))
        {
            if (Constants.TargetLayersOrTag.Contains(LayerMask.LayerToName(hitUnderMouse.collider.gameObject.layer)) || Constants.TargetLayersOrTag.Contains(hitUnderMouse.collider.gameObject.tag))
                pointDirection = hitUnderMouse.collider.transform.position;
            else
                pointDirection = hitUnderMouse.point + offsetShoot;
        }


        Vector3 toCam = new Vector3(-_mainCamera.transform.forward.x, 0, -_mainCamera.transform.forward.z) * (_player.playerWeaponManager.weapon._canon.transform.position.y - pointDirection.y);
        Vector3 direction = new Vector3(pointDirection.x, transform.position.y, pointDirection.z) + toCam;

        transform.LookAt(direction);
    }   

    void Movement()
    {
        _rigidbody.velocity = Quaternion.Euler(0, -45, 0) * new Vector3(moveInput.x, 0, moveInput.y) * speed;

        Vector3 direction = Quaternion.Euler(0, -45, 0) * new Vector3(moveInput.x * 2, 0, moveInput.y * 2);

        direction = transform.InverseTransformDirection(direction);
        _animator.SetFloat("MoveX", direction.x);
        _animator.SetFloat("MoveY", direction.z);
    }
}
