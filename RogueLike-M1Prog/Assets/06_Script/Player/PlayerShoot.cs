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
            
            Shoot(pointDirection);
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

    public override void Shoot(Vector3 shootDirection)
    {
        base.Shoot(shootDirection);
        if(canShoot)
            _playerCanvas._weaponUI.UpdateAmmo(_munitions);
    }

    public void AddAmmo(int ammo)
    {
        _munitions += ammo;
        _playerCanvas._weaponUI.UpdateAmmo(_munitions);
    }
}
