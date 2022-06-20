using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : AHealth
{
    public override void Start()
    {
        if (GameManager.instance.GameSave.Health != 0)
            health = GameManager.instance.GameSave.Health;
        else
            base.Start();
    }

    public override int ModifyHealth(int change)
    {
        base.ModifyHealth(change);
        PlayerCanvas.instance._playerHealthUI.UpdateHealth();
        return health;
    }

    public override bool TakeDamage(int damage, GameObject Sender, DamageType damageTypeSend)
    {
        if (base.TakeDamage(damage, Sender, damageTypeSend))
        {
            Player.Instance.playerStats.DamageTaked += damage;
            PlayerCanvas.instance._playerHealthUI.UpdateHealth();
            PlayerCanvas.instance.HitEffect();
            if(health <= 0)
                GameManager.instance.Save(true);
            return true;
        }
        return false;
    }


    public override bool OnDeath()
    {
        if (base.OnDeath())
        {
            GameManager.instance.OnPlayerDied();
            return true;
        }
        return false;
    }


}
