using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AWeaponManager : MonoBehaviour
{
    public AMainData ownerMainData;

    [Header("External Components")]
    public Transform weaponHandler;

    [Header("Internal Components")]
    public Animator _animator;

    [Header("Equipped weapon")]
    public List<WeaponData> FirstEquippedWeapons;
    public AWeapon weapon;
    [HideInInspector]
    public WeaponData _weaponData;
    [Space]
    public int _munitions = 50;

    [Header("Damages")]
    public int damages;

    [Header("Vision Layer")]
    public LayerMask VisionMask;


    private void Awake()
    {
        ownerMainData = GetComponent<AMainData>();
        ownerMainData.WeaponManager = this;

        InitData();
    }

    protected virtual void Start()
    {
        if (FirstEquippedWeapons.Count > 0)
            EquipWeapon(FirstEquippedWeapons[Random.Range(0, FirstEquippedWeapons.Count)], _munitions);
    }

    public virtual void InitData()
    {
        
    }

    public virtual void DropWeapon(WeaponData _droppedWeapon, int munitions)
    {   
        Vector3 dropPosition = transform.position + transform.forward;
        GameObject droppedWeapon = Instantiate(_droppedWeapon.weaponItem, dropPosition, transform.rotation);
        droppedWeapon.GetComponent<Rigidbody>().AddForce(transform.forward * 100, ForceMode.Impulse);
        droppedWeapon.GetComponent<WeaponItem>().munitions = munitions;
    }

    public virtual void EquipWeapon(WeaponData _newWeapon, int munitions)
    {
        if (_weaponData)
            DropWeapon(_weaponData, _munitions);
        _weaponData = _newWeapon;

        weapon = Instantiate(_newWeapon.weaponPrefab, weaponHandler);
        weapon.Equip(this, _newWeapon, munitions);
        
        _animator.SetTrigger("Equip");
    }

    public virtual void Shoot(Vector3 shootDirection, float additionnalSpray)
    {
        if (!weapon) return;

        weapon.Shoot(shootDirection, additionnalSpray);
    }

    public void AddAmmo(int ammo)
    {
        _munitions += ammo;
        PlayerCanvas.instance._weaponUI.UpdateAmmo(_munitions);
    }
}
