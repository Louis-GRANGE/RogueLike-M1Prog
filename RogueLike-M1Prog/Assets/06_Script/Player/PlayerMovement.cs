using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Player _player;
    DamageTextPool _damageTextPool;

    [Header("External References")]
    Camera _mainCamera;

    [Header("Components")]
    Rigidbody _rigidbody;
    Animator _animator;

    [Header("Metrics")]
    public float speed;
    Vector2 _movement;

    public Vector3 offsetShoot;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = transform.GetChild(0).GetComponent<Animator>();
    }

    private void Start()
    {
        _player = Player.Instance;
        _damageTextPool = DamageTextPool.Instance;
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        UpdateMovementVector();
    }

    private void FixedUpdate()
    {
        LookDirection();
        Movement();
    }

    void LookDirection()
    {
        Vector3 pointDirection = _mainCamera.ScreenToWorldPoint(Input.mousePosition);

        RaycastHit hit;
        if (Physics.Raycast(pointDirection, _mainCamera.transform.forward, out hit, 1000))
        {
            if (hit.transform.CompareTag(Constants.TagEnemy))
                pointDirection = hit.collider.transform.position;
            else
                pointDirection = hit.point + offsetShoot;
        }


        Vector3 toCam = new Vector3(-_mainCamera.transform.forward.x, 0, -_mainCamera.transform.forward.z) * (_player.playerShoot._canon.transform.position.y - pointDirection.y);
        Vector3 direction = new Vector3(pointDirection.x, transform.position.y, pointDirection.z) + toCam;

        transform.LookAt(direction);
    }

    void Movement()
    {
        _rigidbody.velocity = Quaternion.Euler(0, -45, 0) * new Vector3(_movement.x, 0, _movement.y) * speed;

        Vector3 direction = Quaternion.Euler(0, -45, 0) * new Vector3(Input.GetAxisRaw("Horizontal") * 2, 0, Input.GetAxisRaw("Vertical") * 2);

        direction = transform.InverseTransformDirection(direction);

        _animator.SetFloat("MoveX", direction.x);
        _animator.SetFloat("MoveY", direction.z);
    }

    void UpdateMovementVector() => _movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Item/Weapon")
        {
            WeaponItem item = other.transform.parent.GetComponent<WeaponItem>();

            if (item.weaponData == _player.playerShoot._weaponData)
            {
                _player.playerShoot.AddAmmo(item.munitions);
                _damageTextPool.RequestMunitionText(item.transform.position, item.munitions);
                item.Desactivate();
            }
        }
    }
}
