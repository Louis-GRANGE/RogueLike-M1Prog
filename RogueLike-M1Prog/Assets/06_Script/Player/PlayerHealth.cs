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

    public override int ModifyHealth(int change)
    {
        base.ModifyHealth(change);
        _playerHealthUI.UpdateHealth();
        return health;
    }

    public override bool TakeDamage(int damage, GameObject Sender, DamageType damageTypeSend)
    {
        if (base.TakeDamage(damage, Sender, damageTypeSend))
        {
            _playerHealthUI.UpdateHealth();
            _playerCanvas.HitEffect();
            return true;
        }
        return false;
    }
}
