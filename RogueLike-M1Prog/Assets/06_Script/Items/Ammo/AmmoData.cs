using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AmmoData", menuName = "ScriptableObjects/Items/AmmoData")]
public class AmmoData : ScriptableObject
{
    [Header("Models")]
    public AmmoItem ammoItem;
    public Sprite icon;

    [Space, Header("Values")]
    public int ammo;
}
