using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCanvas : Singleton<PlayerCanvas>
{
    [HideInInspector] public WeaponUI _weaponUI;
    [HideInInspector] public PlayerHealthUI _playerHealthUI;

    Animator _hitEffect;

    protected override void Awake()
    {
        base.Awake();
        /*if (!Instance)
            Instance = this;
        else
            Destroy(gameObject);*/

        _weaponUI = GetComponentInChildren<WeaponUI>();
        _playerHealthUI = GetComponentInChildren<PlayerHealthUI>();

        _hitEffect = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        _playerHealthUI.UpdateHealth();
        _weaponUI.UpdateWeapon(Player.Instance.WeaponManager._weaponData, Player.Instance.WeaponManager.weapon._munitions);
    }

    public void HitEffect() => _hitEffect.SetTrigger("Hit");
}