using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AHealth : MonoBehaviour
{
    [Header("Health")]
    [HideInInspector]
    public int health;
    public int maxHealth;
    protected bool IsDeath;

    // For PopUI
    [HideInInspector]
    public DamageTextPool damageTextPool;

    protected virtual void Start()
    {
        health = maxHealth;
        damageTextPool = DamageTextPool.Instance;
    }

    public void SetHealth(int value)
    {
        health = value;
        maxHealth = value;
    }

    public virtual int ModifyHealth(int change)
    {
        health += change;
        if (health <= 0)
        {
            OnDeath();
            Destroy(gameObject);
        }
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        return health;
    }

    private void OnDestroy()
    {
        OnDeath();
    }

    public virtual bool OnDeath()
    {
        bool IsDead = CheckIsDead();
        return IsDead;
    }
    public virtual bool OnDeath(GameObject Sender)
    {
        bool IsDead = CheckIsDead();
        return IsDead;
    }

    public virtual void TakeDamage(int damage, GameObject Sender)
    {
        health -= damage;
        if (health <= 0)
        {
            if (!OnDeath(Sender))
                Destroy(gameObject);
        }
    }

    private bool CheckIsDead()
    {
        if (IsDeath)
        {
            return true;
        }
        else
        {
            IsDeath = true;
            return false;
        }
    }
}
