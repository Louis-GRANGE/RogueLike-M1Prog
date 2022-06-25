using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SemiAutomaticWeapon : AWeapon
{
    [Header("Effects")]
    protected GameObject _laserFX;
    protected Transform _laserPool;
    public GameObject hitFX;
    protected Transform _hitPool;

    public override void Equip(AWeaponManager owner, WeaponData weaponData, int munitions)
    {
        base.Equip(owner, weaponData, munitions);

        if (_laserPool)
            Destroy(_laserPool.gameObject);

        _laserPool = new GameObject().transform;
        _laserPool.parent = transform;
        _laserPool.gameObject.SetActive(false);
        for (int i = 0; i < 50; i++)
        {
            GameObject newLaser = Instantiate(weaponData.munitionEffect, transform.position, transform.rotation, _laserPool);
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
    }


    public override bool Shoot(Vector3 shootDirection, float additionnalSpray = 0)
    {
        if (!base.Shoot(shootDirection, additionnalSpray))
            return false;

        float sprayX = additionnalSpray + weaponData.spray.x;
        float sprayY = additionnalSpray + weaponData.spray.y;
        shootDirection = shootDirection + transform.right * Random.Range(-sprayX / 10, sprayX / 10) + transform.up * Random.Range(-sprayY / 10, sprayY / 10);
        Vector3 TargetPoint = shootDirection + transform.position;

        RaycastHit hit;
        if (Physics.Raycast(_canon.transform.position, shootDirection, out hit, 1000))
        {
            if (!hit.transform.CompareTag(_owner.ownerMainData.transform.tag))
            {
                AHealth Health;
                if (hit.collider.TryGetComponent(out Health))
                    Health.TakeDamage(_damages, _owner.ownerMainData.gameObject, weaponData.DealDamageType);
                _laserPool.GetChild(0).GetComponent<LaserEffect>().DrawLine(_cannonFire.transform.position, hit.point);
                _hitPool.GetChild(0).GetComponent<HitEffect>().DrawParticle(hit.point);

                _cannonFire.Play();
            }
        }
        else
        {
            _laserPool.GetChild(0).GetComponent<LaserEffect>().DrawLine(_cannonFire.transform.position, _cannonFire.transform.position + shootDirection * 100);
        }

        PlaySound();
        return true;
    }
}
