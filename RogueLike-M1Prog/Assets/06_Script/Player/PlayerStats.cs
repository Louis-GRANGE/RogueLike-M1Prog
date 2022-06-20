using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int NumberKills;
    public int DamagesDeals;
    public int DamageTaked;

    private void Start()
    {
        if (GameManager.instance.GameSave.NumberKills != 0)
            NumberKills = GameManager.instance.GameSave.NumberKills;
        else if (GameManager.instance.GameSave.DamagesDealt != 0)
            DamagesDeals = GameManager.instance.GameSave.NumberKills;
        else if (GameManager.instance.GameSave.DamagesTaken != 0)
            DamageTaked = GameManager.instance.GameSave.NumberKills;
    }
}
