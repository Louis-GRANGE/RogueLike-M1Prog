using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int NumberKills;
    public int DamagesDeals;
    public int DamageTaked;
    SOSaveGame saveGame;

    private void Start()
    {
        saveGame = SaveManager.instance.GetSave<SOSaveGame>();

        if (saveGame)
        {
            if (saveGame.CanContinue)
            {
                NumberKills = saveGame.NumberKills;
                DamagesDeals = saveGame.DamagesDealt;
                DamageTaked = saveGame.DamagesTaken;
            }
            else
            {
                NumberKills = 0;
                DamagesDeals = 0;
                DamageTaked = 0;
            }
        }
    }
}
