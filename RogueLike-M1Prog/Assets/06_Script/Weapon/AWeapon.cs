using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AWeapon : MonoBehaviour
{
    [Header("External Components")]
    public Transform weaponHandler;
    protected Transform _canon;

    [Header("Internal Components")]
    protected ParticleSystem _cannonFire;
    protected Animator _animator;

    [Header("Equipped weapon")]
    protected Weapon _weapon;
    protected WeaponData _weaponData;
    protected int _munitions = 50;

    [Header("Effects")]
    protected GameObject _laserFX;
    protected Transform _laserPool;
    public GameObject hitFX;
    protected Transform _hitPool;

    [Header("FireRate")]
    public float fireRateLatency;
    protected float _fireRateTime;

    [Header("Damages")]
    public int damages;

    private void Awake()
    {
        Init(transform.GetChild(0).GetComponent<Animator>());
    }

    public virtual void Init(Animator animator) { _animator = animator; }

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
        if (!_weapon || _munitions <= 0)
            return;
        if (_fireRateTime < fireRateLatency)
        {
            _fireRateTime += Time.deltaTime;
            return;
        }

        _munitions -= 1;
        _fireRateTime = 0;

        RaycastHit hit;
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
