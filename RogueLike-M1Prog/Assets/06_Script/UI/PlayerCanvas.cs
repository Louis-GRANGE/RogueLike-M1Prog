using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCanvas : MonoBehaviour
{
    [HideInInspector] public static PlayerCanvas Instance;

    [HideInInspector] public WeaponUI _weaponUI;

    private void Awake()
    {
        if (!Instance)
            Instance = this;
        else
            Destroy(gameObject);

        _weaponUI = GetComponentInChildren<WeaponUI>();
    }
}
