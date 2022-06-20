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
    AItem _lastInteractedTrigger;
    AItem _lastInteracted;

    bool IsFiring;
    bool IsInteract;

    [HideInInspector]
    public Vector3 TargetShootPos;
    [HideInInspector]
    public bool HaveTarget;

    /*private void Awake()
    {
        //_animator = transform.GetChild(0).GetComponent<Animator>();
    }*/

    public void Fire(InputAction.CallbackContext context) { IsFiring = context.performed; }
    public void Interact(InputAction.CallbackContext context) { Debug.Log("Interact"); IsInteract = context.action.WasPressedThisFrame(); }

    protected override void Start()
    {
        _playerCanvas = PlayerCanvas.instance;

        _mainCamera = Camera.main;
        base.Start();
        //EquipWeapon(Resources.Load<WeaponData>("WeaponData/Automatic"), _munitions);
    }

    private void Update()
    {
        RaycastHit hit = Player.Instance.playerMovement.hitUnderMouse;
        if (IsFiring)
        {
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

        if (hit.collider != null)
        {
            AItem item;
            if (hit.collider.TryGetComponent<AItem>(out item))
            {
                InteractMouse(item);
            }
            else if (_lastInteracted)
            {
                _lastInteracted.HideShown();
                _lastInteracted = null;
            }
        }
        else if(_lastInteracted)
            _lastInteracted.HideShown();
    }

    public void InteractMouse(AItem NewItem)
    {
        if(NewItem != _lastInteracted)
        {
            if(_lastInteracted && _lastInteracted)
                _lastInteracted.HideShown();
            _lastInteracted = NewItem;
            if (NewItem)
                _lastInteracted.ActualizeShown();
        }
        
        if (NewItem && Vector3.Distance(transform.position, NewItem.transform.position) < 3f && IsInteract)
        {
            WeaponItem weaponItem = (WeaponItem)NewItem;
        
            Destroy(_weapon);
            EquipWeapon(weaponItem.weaponData, weaponItem.munitions);
            weaponItem.Desactivate();
        }
    }
    public void InteractTrigger(AItem NewItem)
    {
        if (NewItem != _lastInteractedTrigger)
        {
            if (_lastInteractedTrigger && _lastInteractedTrigger)
                _lastInteractedTrigger.HideShown();
            _lastInteractedTrigger = NewItem;
            if (NewItem)
                _lastInteractedTrigger.ActualizeShown();
        }

        if (NewItem && IsInteract)
        {
            WeaponItem weaponItem = (WeaponItem)NewItem;

            Destroy(_weapon);
            EquipWeapon(weaponItem.weaponData, weaponItem.munitions);
            weaponItem.Desactivate();
        }
    }

    public override void EquipWeapon(WeaponData _newWeapon, int munitions)
    {
        base.EquipWeapon(_newWeapon, munitions);
        if (PlayerCanvas.instance)
            PlayerCanvas.instance._weaponUI.UpdateWeapon(_newWeapon, _munitions);
    }

    public override void Shoot(Vector3 shootDirection, float additionnalSpray = 0)
    {
        if(_munitions > 0)
        {
            base.Shoot(shootDirection, additionnalSpray);
            if (canShoot)
            {
                _munitions -= 1;
                PlayerCanvas.instance._weaponUI.UpdateAmmo(_munitions);
            }
        }
    }
}
