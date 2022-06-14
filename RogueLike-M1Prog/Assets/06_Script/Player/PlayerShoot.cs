using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : AWeapon
{
    PlayerCanvas _playerCanvas;

    [Header("External References")]
    Camera _mainCamera;

    [Header("Interaction")]
    Collider _lastInteracted;

    bool IsFiring;
    bool IsInteract;

    /*private void Awake()
    {
        //_animator = transform.GetChild(0).GetComponent<Animator>();
    }*/

    public void Fire(InputAction.CallbackContext context) { IsFiring = context.performed; }
    public void Interact(InputAction.CallbackContext context) { IsInteract = context.action.WasPressedThisFrame(); }

    protected override void Start()
    {
        _playerCanvas = PlayerCanvas.Instance;

        _mainCamera = Camera.main;
        base.Start();
        //EquipWeapon(Resources.Load<WeaponData>("WeaponData/Automatic"), _munitions);
    }

    private void Update()
    {
        if (IsFiring)
        {
            RaycastHit hit = Player.Instance.playerMovement.hitUnderMouse;
            if (hit.collider != null)
            {
                // IF you can target a targetable object
                if (Constants.TargetLayersOrTag.Contains(LayerMask.LayerToName(hit.collider.gameObject.layer)) || Constants.TargetLayersOrTag.Contains(hit.collider.gameObject.tag))
                {
                    Shoot((hit.transform.position - _canon.position).normalized);
                    return;
                }
            }
            Shoot(new Vector3(_canon.forward.x, 0, _canon.forward.z));
        }

        Interact();
    }

    public void Interact()
    {
        RaycastHit hit = Player.Instance.playerMovement.hitUnderMouse;
        if (hit.collider != null)
        {
            if(hit.collider != _lastInteracted)
            {
                if(_lastInteracted && _lastInteracted.GetComponent<WeaponItem>())
                    _lastInteracted.GetComponent<WeaponItem>().HideShown();
                _lastInteracted = hit.collider;
                if (hit.collider.GetComponent<WeaponItem>())
                    _lastInteracted.GetComponent<WeaponItem>().ActualizeShown();
            }
                

            if (hit.collider.GetComponent<WeaponItem>() && Vector3.Distance(transform.position, hit.point) < 3f && IsInteract)
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

    public override void Shoot(Vector3 shootDirection, float additionnalSpray = 0)
    {
        if(_munitions > 0)
        {
            base.Shoot(shootDirection, additionnalSpray);
            if (canShoot)
            {
                _munitions -= 1;
                _playerCanvas._weaponUI.UpdateAmmo(_munitions);
            }
        }
    }
}
