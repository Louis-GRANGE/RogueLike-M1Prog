using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "ScriptableObjects/WeaponData")]
public class WeaponData : ScriptableObject
{
    [Header("Models")]
    public GameObject weaponPrefab;
    public GameObject weaponItem;

    [Space, Header("Values")]
    public int damages;
    public float fireLatency;

    [Space, Header("Effects")]
    public GameObject munitionEffect;
}
