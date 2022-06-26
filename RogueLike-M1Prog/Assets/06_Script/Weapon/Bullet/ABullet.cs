using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ABullet : MonoBehaviour
{
    [HideInInspector]
    public GameObject Sender;
    [HideInInspector]
    public WeaponData weaponData;
}
