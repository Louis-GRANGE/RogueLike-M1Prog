using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[System.Flags]
public enum DamageType
{
    None = 0, // Custom name for "Nothing" option
    Bullet = 1 << 0,
    Explosion = 1 << 1,
    Poison = 1 << 2,
    All = ~0, // Custom name for "Everything" option
}

public static class Constants
{
    public const string TagEnemy = "Enemy";
    public const string TagPlayer = "Player";
    public const int MainMenu = 0;
    public const int GameLevel = 1;

    public static readonly List<string> TargetLayersOrTag = new List<string> { "CanBeTarget", "Enemy" };
}
