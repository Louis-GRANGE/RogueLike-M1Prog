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

    protected virtual void Start()
    {
        health = maxHealth;
    }

    public void SetHealth(int value)
    {
        health = value;
        maxHealth = value;
    }

    public int ModifyHealth(int change)
    {
        health += change;
        if (health <= 0)
        {
            OnDeath();
            Destroy(gameObject);
        }
        return health;
    }

    private void OnDestroy()
    {
        if (!IsDeath)
        {
            IsDeath = true;
            OnDeath();
        }
    }

    public virtual void OnDeath()
    {
        if (IsDeath)
            return;
        IsDeath = true;
        Destroy(gameObject);
    }
    public virtual void OnDeath(GameObject Sender)
    {
        if (IsDeath)
            return;
        IsDeath = true;
        Destroy(gameObject);
    }

    public virtual void TakeDamage(int damage, GameObject Sender)
    {
        health -= damage;
        if (health <= 0)
        {
            OnDeath(Sender);
            Destroy(gameObject);
        }
    }
}
