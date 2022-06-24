using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AWeapon : MonoBehaviour
{
    public Transform _canon;

    public AudioSource Sound;
    public bool isAuto;
    
    protected ParticleSystem _cannonFire;
    protected int _damages;
    protected int _munitions;
    protected WeaponData _weaponData;
    protected AWeaponManager _owner;

    [Header("FireRate")]
    [HideInInspector]
    public float fireRateLatency;
    protected float _fireRateTime;
    public bool canShoot = true;

    public virtual void PlaySound()
    {
        if(isAuto && SoundManager.Instance.RequestAutoSound())
            Sound.Play();
        else if (!isAuto && SoundManager.Instance.RequestSemiAutoSound())
            Sound.Play();
    }

    public virtual void Equip(AWeaponManager owner, WeaponData weaponData, int munitions)
    {
        _owner = owner;
        _cannonFire = _canon.GetChild(0).GetComponent<ParticleSystem>();

        fireRateLatency = weaponData.fireLatency;
        _damages = weaponData.damages;
        _munitions = munitions;

        _fireRateTime = fireRateLatency;

        _weaponData = weaponData;
    }

    public virtual bool Shoot(Vector3 shootDirection, float additionnalSpray = 0)
    {
        canShoot = true;

        if (_fireRateTime < fireRateLatency)
        {
            canShoot = false;
            _fireRateTime += Time.deltaTime;
            return false;
        }
        _fireRateTime = 0;
        return true;
    }
}
