using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : AHealth
{
    [Header("External References")]
    PlayerCanvas _playerCanvas;
    PlayerHealthUI _playerHealthUI;

    protected override void Start()
    {
        base.Start();
        _playerCanvas = PlayerCanvas.Instance;
        _playerHealthUI = _playerCanvas._playerHealthUI;
        _playerHealthUI._memorizedHealth = health;
    }

    public override void TakeDamage(int damage, GameObject Sender)
    {
        base.TakeDamage(damage, Sender);
        _playerHealthUI.UpdateHealth();
        _playerCanvas.HitEffect();
    }
}
