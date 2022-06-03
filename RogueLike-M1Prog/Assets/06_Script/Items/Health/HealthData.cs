using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealthData", menuName = "ScriptableObjects/HealthData")]
public class HealthData : ScriptableObject
{
    [Header("Models")]
    public HealItem healItem;
    public Sprite icon;

    [Space, Header("Values")]
    public int health;
}
