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
    public int _munitions;
    public WeaponData weaponData;
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

        this.weaponData = weaponData;
    }

    public virtual bool Shoot(Vector3 shootDirection, float additionnalSpray = 0)
    {
        canShoot = true;

        if (_fireRateTime < fireRateLatency)
        {
            canShoot = false;
            return false;
        }
        _fireRateTime = 0;
        return true;
    }

    protected virtual void Update()
    {
        if (_fireRateTime < fireRateLatency)
            _fireRateTime += Time.deltaTime;
    }
}
