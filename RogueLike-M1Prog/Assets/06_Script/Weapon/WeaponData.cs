using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "ScriptableObjects/WeaponData")]
public class WeaponData : ScriptableObject
{
    [Header("Models")]
    public GameObject weaponPrefab;
    public GameObject weaponItem;
    public Sprite icon;

    [Space, Header("Values")]
    public int damages;
    public float fireLatency;
    [Tooltip("Weapon Spray 0 = NoSpray, 1 = SprayMax")]
    public float spray;

    [Space, Header("Effects")]
    public GameObject munitionEffect;
}
