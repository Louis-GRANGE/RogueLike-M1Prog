using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "ScriptableObjects/WeaponData")]
public class WeaponData : ScriptableObject
{
    [Header("Models")]
    public AWeapon weaponPrefab;
    public GameObject AmmoPrefab;
    public GameObject weaponItem;
    public Sprite icon;

    [Space, Header("Values")]
    public int damages;
    public float fireLatency;
    [Tooltip("Weapon Spray 0 = NoSpray")]
    public Vector2 spray;
    public DamageType DealDamageType;

    [Space, Header("Effects")]
    public GameObject munitionEffect;
}
