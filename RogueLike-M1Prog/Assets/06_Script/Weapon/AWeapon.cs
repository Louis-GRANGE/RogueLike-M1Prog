using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AWeapon : MonoBehaviour
{
    protected AMainData ownerMainData;

    [Header("External Components")]
    public Transform weaponHandler;
    [HideInInspector]
    public Transform _canon;

    [Header("Internal Components")]
    protected ParticleSystem _cannonFire;
    public Animator _animator;

    [Header("Equipped weapon")]
    public string pathEquipWeapon = "WeaponData/Automatic";
    protected Weapon _weapon;
    [HideInInspector]
    public WeaponData _weaponData;
    public int _munitions = 50;

    [Header("Effects")]
    protected GameObject _laserFX;
    protected Transform _laserPool;
    public GameObject hitFX;
    protected Transform _hitPool;

    [Header("FireRate")]
    public float fireRateLatency;
    protected float _fireRateTime;
    public bool canShoot = true;

    [Header("Damages")]
    public int damages;


    private void Awake()
    {
        ownerMainData = GetComponent<AMainData>();
        ownerMainData.WeaponManager = this;

        InitData();
    }

    protected virtual void Start()
    {
        EquipWeapon(Resources.Load<WeaponData>(pathEquipWeapon), _munitions);
    }

    public virtual void InitData()
    {
        
    }

    public virtual void DropWeapon(WeaponData _droppedWeapon, int munitions)
    {   
        Vector3 dropPosition = transform.position + transform.forward;
        GameObject droppedWeapon = Instantiate(_droppedWeapon.weaponItem, dropPosition, transform.rotation);

        droppedWeapon.GetComponent<WeaponItem>().munitions = munitions;
    }

    public virtual void EquipWeapon(WeaponData _newWeapon, int munitions)
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

        if (_hitPool)
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

    public virtual void Shoot(Vector3 shootDirection)
    {
        canShoot = true;
        if (!_weapon || _munitions <= 0)
        {
            canShoot = false;
            return;
        }
        if (_fireRateTime < fireRateLatency)
        {
            canShoot = false;
            _fireRateTime += Time.deltaTime;
            return;
        }

        _munitions -= 1;
        _fireRateTime = 0;

        shootDirection = new Vector3(shootDirection.x + Random.Range(-0.3f, 0.3f), 0, shootDirection.z + Random.Range(-0.3f, 0.3f));
        Vector3 TargetPoint = shootDirection + transform.position;

        //Change Rotation Of GameObject to the direction of shoot
        transform.LookAt(new Vector3(TargetPoint.x, transform.position.y, TargetPoint.z));

        RaycastHit hit;
        if (Physics.Raycast(_canon.transform.position, shootDirection, out hit, 1000))
        {
            if (!hit.transform.CompareTag(ownerMainData.transform.tag))
            {
                AHealth Health = hit.collider.GetComponent<AHealth>();
                if (Health)
                    Health.TakeDamage(damages, ownerMainData.gameObject);
                _laserPool.GetChild(0).GetComponent<LaserEffect>().DrawLine(_cannonFire.transform.position, hit.point);
                _hitPool.GetChild(0).GetComponent<HitEffect>().DrawParticle(hit.point);

                _cannonFire.Play();
            }
        }
        else
        {
            _laserPool.GetChild(0).GetComponent<LaserEffect>().DrawLine(_cannonFire.transform.position, _cannonFire.transform.position + shootDirection * 100);
        }
    }
}
