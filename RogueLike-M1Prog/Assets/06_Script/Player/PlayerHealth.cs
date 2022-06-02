using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : AHealth
{
    [Header("External References")]
    PlayerHealthUI _playerHealthUI;

    protected override void Start()
    {
        base.Start();
        _playerHealthUI = PlayerCanvas.Instance._playerHealthUI;
        _playerHealthUI._memorizedHealth = health;
    }

    public override void TakeDamage(int damage, GameObject Sender)
    {
        base.TakeDamage(damage, Sender);
        _playerHealthUI.UpdateHealth();
    }
}
