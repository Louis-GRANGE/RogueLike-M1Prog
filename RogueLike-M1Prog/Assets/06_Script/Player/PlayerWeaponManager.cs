using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeaponManager : AWeaponManager
{
    PlayerCanvas _playerCanvas;
    PlayerMovement _playerMovement;

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
    public Transform HaveTarget;
    //[HideInInspector]
    //public bool HaveTarget;

    /*private void Awake()
    {
        //_animator = transform.GetChild(0).GetComponent<Animator>();
    }*/

    public void Fire(InputAction.CallbackContext context) { IsFiring = context.performed; }
    public void Interact(InputAction.CallbackContext context) { IsInteract = context.started; } // TORECHECK
    public void UnInteract(InputAction.CallbackContext context) { IsInteract = false; }

    protected override void Start()
    {
        _playerCanvas = PlayerCanvas.instance;
        _playerMovement = GetComponent<PlayerMovement>();

        _mainCamera = Camera.main;
        if(SaveManager.instance && SaveManager.instance.GetSave<SOSaveGame>() && SaveManager.instance.GetSave<SOSaveGame>().CanContinue && SaveManager.instance.GetSave<SOSaveGame>().EquipedWeapon)
            EquipWeapon(SaveManager.instance.GetSave<SOSaveGame>().EquipedWeapon, SaveManager.instance.GetSave<SOSaveGame>().Munitions);
        else
            base.Start();
        //EquipWeapon(Resources.Load<WeaponData>("WeaponData/Automatic"), _munitions);
    }

    private void Update()
    {
        RaycastHit hit = _playerMovement.hitUnderMouse;//Player.Instance.playerMovement.hitUnderMouse;
        if (hit.transform)
        {
            if (Constants.TargetLayersOrTag.Contains(LayerMask.LayerToName(hit.collider.gameObject.layer)) || Constants.TargetLayersOrTag.Contains(hit.collider.gameObject.tag) || (hit.transform.gameObject.GetComponent<AHealth>() && hit.transform.gameObject.GetComponent<AHealth>().CanBeDamage(weapon.weaponData.DealDamageType)))
            {
                HaveTarget = hit.transform;
                TargetShootPos = hit.transform.position;
            }
            else
            {
                HaveTarget = null;
                TargetShootPos = hit.point;
            }
        }


        if (IsFiring)
        {
            if (hit.collider != null)
            {
                // IF you can target a targetable object
                if (HaveTarget)
                {
                    Shoot((hit.transform.position - weapon._canon.position).normalized);
                    return;
                }
            }
            Shoot(new Vector3(weapon._canon.forward.x, 0, weapon._canon.forward.z));
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
            IsInteract = false;
            if (NewItem.GetType() == typeof(WeaponItem))
            {

                Debug.Log("Destroy a weapon: " + weapon.name);
                WeaponItem weaponItem = (WeaponItem)NewItem;
                Destroy(weapon.gameObject);
                Debug.Log("munition: " + weaponItem.munitions);
                EquipWeapon(weaponItem.weaponData, weaponItem.munitions);
                weaponItem.Desactivate();
            }
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
            IsInteract = false;
            if (NewItem.GetType() == typeof(WeaponItem))
            {
                WeaponItem weaponItem = (WeaponItem)NewItem;
                Destroy(weapon.gameObject);
                Debug.Log("munition: " + weaponItem.munitions);
                EquipWeapon(weaponItem.weaponData, weaponItem.munitions);
                weaponItem.Desactivate();
            }
        }
    }

    public override void EquipWeapon(WeaponData _newWeapon, int munitions)
    {
        base.EquipWeapon(_newWeapon, munitions);
        if (PlayerCanvas.instance)
            PlayerCanvas.instance._weaponUI.UpdateWeapon(_newWeapon, weapon._munitions);
        
        if(SoundManager.Instance)
            SoundManager.Instance.RequestSoundEffect(transform.position, SoundType.Item);
    }

    public override void Shoot(Vector3 shootDirection, float additionnalSpray = 0)
    {
        if(weapon._munitions > 0)
        {
            base.Shoot(shootDirection, additionnalSpray);
            if (weapon.canShoot)
            {
                weapon._munitions -= 1;
                PlayerCanvas.instance._weaponUI.UpdateAmmo(weapon._munitions);
            }
        }
    }
}
