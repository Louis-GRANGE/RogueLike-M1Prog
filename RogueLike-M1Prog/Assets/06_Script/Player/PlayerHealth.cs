using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : AHealth
{
    PlayerStats playerStats;
    public override void Start()
    {
        playerStats = GetComponent<PlayerStats>();
        if (SaveManager.instance.GetSave<SOSaveGame>().CanContinue)
            health = SaveManager.instance.GetSave<SOSaveGame>().Health;
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
        if (CanBeDamage(damageTypeSend))
        {
            health -= damage;
            playerStats.DamageTaked += damage;
            PlayerCanvas.instance._playerHealthUI.UpdateHealth();
            PlayerCanvas.instance.HitEffect();
            if (health <= 0)
            {
                if (!OnDeath(Sender))
                {
                    GameManager.instance.Save(true);
                    Destroy(gameObject);
                }
            }
            return true;
        }
        return false;
    }


    public override bool OnDeath()
    {
        if (base.OnDeath() && GameManager.instance)
        {
            if(health <= 0)
                GameManager.instance.Save(true);
            GameManager.instance.OnPlayerDied();
            return true;
        }
        return false;
    }


}
