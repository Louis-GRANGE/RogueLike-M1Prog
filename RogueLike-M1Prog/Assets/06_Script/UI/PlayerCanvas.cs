using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCanvas : MonoBehaviour
{
    [HideInInspector] public static PlayerCanvas Instance;

    [HideInInspector] public WeaponUI _weaponUI;
    [HideInInspector] public PlayerHealthUI _playerHealthUI;

    Animator _hitEffect;

    private void Awake()
    {
        if (!Instance)
            Instance = this;
        else
            Destroy(gameObject);

        _weaponUI = GetComponentInChildren<WeaponUI>();
        _playerHealthUI = GetComponentInChildren<PlayerHealthUI>();

        _hitEffect = GetComponentInChildren<Animator>();
    }

    public void HitEffect() => _hitEffect.SetTrigger("Hit");
}