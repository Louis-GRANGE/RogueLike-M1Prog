using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealthData", menuName = "ScriptableObjects/Items/HealData")]
public class HealData : ScriptableObject
{
    [Header("Models")]
    public HealItem healItem;
    public Sprite icon;

    [Space, Header("Values")]
    public int health;
}
