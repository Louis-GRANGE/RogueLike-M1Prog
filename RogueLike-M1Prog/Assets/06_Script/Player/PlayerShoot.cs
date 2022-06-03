using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : AWeapon
{
    PlayerCanvas _playerCanvas;

    [Header("External References")]
    Camera _mainCamera;

    [Header("Interaction")]
    Collider _lastInteracted;

    /*private void Awake()
    {
        //_animator = transform.GetChild(0).GetComponent<Animator>();
    }*/

    protected override void Start()
    {
        _playerCanvas = PlayerCanvas.Instance;

        _mainCamera = Camera.main;
        base.Start();
        //EquipWeapon(Resources.Load<WeaponData>("WeaponData/Automatic"), _munitions);
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Shoot(_canon.forward);
        }

        Interact();
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

    public override void EquipWeapon(WeaponData _newWeapon, int munitions)
    {
        base.EquipWeapon(_newWeapon, munitions);
        _playerCanvas._weaponUI.UpdateWeapon(_newWeapon, _munitions);
    }

    public override void Shoot(Vector3 shootDirection, bool consumeAmmo = true)
    {
        base.Shoot(shootDirection, consumeAmmo);
        if(canShoot)
            _playerCanvas._weaponUI.UpdateAmmo(_munitions);
    }

    public void AddAmmo(int ammo)
    {
        _munitions += ammo;
        _playerCanvas._weaponUI.UpdateAmmo(_munitions);
    }
}
