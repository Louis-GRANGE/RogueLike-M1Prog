using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [Header("External References")]
    Camera _mainCamera;

    [Header("External Components")]
    public Transform weaponHandler;
    Transform _canon;

    [Header("Internal Components")]
    ParticleSystem _cannonFire;
    Animator _animator;

    [Header("Equipped weapon")]
    Weapon _weapon;
    WeaponData _weaponData;
    int _munitions = 50;

    [Header("Effects")]
    GameObject _laserFX;
    Transform _laserPool;
    public GameObject hitFX;
    Transform _hitPool;

    [Header("FireRate")]
    public float fireRateLatency;
    float _fireRateTime;

    [Header("Damages")]
    public int damages;

    [Header("Interaction")]
    Collider _lastInteracted;

    private void Awake()
    {
        _animator = transform.GetChild(0).GetComponent<Animator>();
    }

    private void Start()
    {
        _mainCamera = Camera.main;
        EquipWeapon(Resources.Load<WeaponData>("WeaponData/Automatic"), _munitions);
    }

    private void Update()
    {
        if(_weapon && _munitions > 0)
            Shoot();

        Interact();
    }

    public void Shoot()
    {
        if(_fireRateTime < fireRateLatency)
        {
            _fireRateTime += Time.deltaTime;
        }

        else if (Input.GetMouseButton(0))
        {
            _munitions -= 1;
            _fireRateTime = 0;
            Vector3 pointDirection = _mainCamera.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(pointDirection, _mainCamera.transform.forward, out hit, 1000))
            {
                pointDirection = hit.point;
                if (hit.collider.GetComponent<EnemyHealth>())
                    pointDirection = hit.collider.transform.position;
            }

            Vector3 toCam = new Vector3(-_mainCamera.transform.forward.x, 0, -_mainCamera.transform.forward.z) * (_canon.transform.position.y - pointDirection.y);
            pointDirection = new Vector3(pointDirection.x + Random.Range(-0.3f, 0.3f), _canon.transform.position.y, pointDirection.z + Random.Range(-0.3f, 0.3f)) + toCam;

            Vector3 shootDirection = (pointDirection - _canon.transform.position).normalized;
            if (Physics.Raycast(_canon.transform.position, shootDirection, out hit, 1000))
            {
                EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();
                if (enemyHealth)
                    enemyHealth.TakeDamage(damages);
                _laserPool.GetChild(0).GetComponent<LaserEffect>().DrawLine(_cannonFire.transform.position, hit.point);
                _hitPool.GetChild(0).GetComponent<HitEffect>().DrawParticle(hit.point);
            }
            else
            {
                _laserPool.GetChild(0).GetComponent<LaserEffect>().DrawLine(_cannonFire.transform.position, _cannonFire.transform.position + shootDirection * 100);
            }
            
            _cannonFire.Play();
        }
    }

    public void EquipWeapon(WeaponData _newWeapon, int munitions)
    {
        if (_weaponData)
            DropWeapon(_weaponData, _munitions);
        _weaponData = _newWeapon;

        _weapon = Instantiate(_newWeapon.weaponPrefab, weaponHandler).GetComponent<Weapon>();
        _canon = _weapon.canon;
        _cannonFire = _canon.GetChild(0).GetComponent<ParticleSystem>();

        fireRateLatency = _newWeapon.fireLatency;
        damages = _newWeapon.damages;
        _munitions = munitions;

        if (_laserPool)
            Destroy(_laserPool.gameObject);

        _laserPool = new GameObject().transform;
        _laserPool.parent = transform;
        _laserPool.gameObject.SetActive(false);
        for (int i = 0; i < 50; i++)
        {
            GameObject newLaser = Instantiate(_newWeapon.munitionEffect, transform.position, transform.rotation, _laserPool);
            newLaser.GetComponent<LaserEffect>().Initiate(_laserPool);
        }

        if(_hitPool)
            Destroy(_hitPool.gameObject);

        _hitPool = new GameObject().transform;
        _hitPool.parent = transform;
        _hitPool.gameObject.SetActive(false);
        for (int i = 0; i < 50; i++)
        {
            GameObject newHit = Instantiate(hitFX, transform.position, transform.rotation, _hitPool);
            newHit.GetComponent<HitEffect>().Initiate(_hitPool);
        }

        _fireRateTime = fireRateLatency;

        _animator.SetTrigger("Equip");
    }

    public void DropWeapon(WeaponData _droppedWeapon, int munitions)
    {
        Vector3 dropPosition = transform.position + transform.forward;
        GameObject droppedWeapon = Instantiate(_droppedWeapon.weaponItem, dropPosition, transform.rotation);

        droppedWeapon.GetComponent<WeaponItem>().munitions = munitions;

        droppedWeapon.GetComponent<Rigidbody>().AddForce(transform.forward * 100, ForceMode.Impulse);
    }

    public void Interact()
    {
        Vector3 pointDirection = _mainCamera.ScreenToWorldPoint(Input.mousePosition);

        RaycastHit hit;
        if (Physics.Raycast(pointDirection, _mainCamera.transform.forward, out hit, 1000))
        {
            if(hit.collider != _lastInteracted)
            {
                if(_lastInteracted && _lastInteracted.GetComponent<WeaponItem>())
                    _lastInteracted.GetComponent<WeaponItem>().HideShown();
                _lastInteracted = hit.collider;
                if (hit.collider.GetComponent<WeaponItem>())
                    _lastInteracted.GetComponent<WeaponItem>().ActualizeShown();
            }
                

            if (hit.collider.GetComponent<WeaponItem>() && Vector3.Distance(transform.position, hit.point) < 3f && Input.GetKeyDown(KeyCode.E))
            {
                WeaponItem weaponItem = hit.collider.GetComponent<WeaponItem>();

                Destroy(_weapon);
                EquipWeapon(weaponItem.weaponData, weaponItem.munitions);
                weaponItem.Desactivate();
            }
        }
        else
        {
            if(_lastInteracted && _lastInteracted.GetComponent<WeaponItem>())
            {
                _lastInteracted.GetComponent<WeaponItem>().HideShown();
                _lastInteracted = null;
            }
        }
    }
}
