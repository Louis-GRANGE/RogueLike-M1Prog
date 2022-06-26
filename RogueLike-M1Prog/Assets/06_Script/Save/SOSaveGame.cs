using System.IO;
using UnityEngine;


[CreateAssetMenu(fileName = "SaveGame", menuName = "ScriptableObjects/Save/SaveGame")]
public class SOSaveGame : ASaveSO
{
    public bool CanContinue = false;

    public WeaponData EquipedWeapon;
    public int Health = 0;
    public int Munitions = 0;
    public int Difficulty = 0;
    public int Seed = 0;

    [Header("PlayerStats")]
    public int NumberKills = 0;
    public int DamagesDealt = 0;
    public int DamagesTaken = 0;

    public void SetValues(bool isAlive = false, WeaponData equipedWeapon = null, int health = 0, int munitions = 0, int difficulty = 0, int seed = 0, int numberKills = 0, int damageDealt = 0, int damageTaken = 0)
    {
        CanContinue = isAlive;
        EquipedWeapon = equipedWeapon;
        Health = health;
        Munitions = munitions;
        Difficulty = difficulty;
        Seed = seed;

        NumberKills = numberKills;
        DamagesDealt = damageDealt;
        DamagesTaken = damageTaken;

        Save();
    }
}
