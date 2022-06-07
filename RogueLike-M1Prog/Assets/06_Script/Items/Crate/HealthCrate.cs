using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthCrate : AHealth
{
    [Header("External")]
    public CrateBraked prefabBrakedCrate;
    public AItem[] itemCanBeSpawn;
    private int _indexRandomSpawn;

    protected override void Start()
    {
        base.Start();
        _indexRandomSpawn = Random.Range(0, itemCanBeSpawn.Length);
    }

    public override bool TakeDamage(int damage, GameObject Sender, DamageType damageTypeSend)
    {
        if (base.TakeDamage(damage, Sender, damageTypeSend))
        {
            damageTextPool.RequestDamageText(transform.position, damage);
            return true;
        }
        return false;
    }

    private void Update()
    {

    }

    public override bool OnDeath(GameObject Sender)
    {
        if (base.OnDeath(Sender))
            return true;

        if (gameObject.activeInHierarchy)
        {
            Transform tmp;
            tmp = Instantiate(prefabBrakedCrate, transform.position, transform.rotation).transform;
            tmp.parent = transform.parent;
            tmp = Instantiate(itemCanBeSpawn[_indexRandomSpawn], transform.position, transform.rotation).transform;
            tmp.parent = transform.parent;
        }
        Destroy(gameObject, 0.1f);
        return true;
    }

    public override bool OnDeath()
    {
        if (base.OnDeath())
            return true;

        if (gameObject.activeInHierarchy)
        {
            Transform tmp;
            tmp = Instantiate(prefabBrakedCrate, transform.position, transform.rotation).transform;
            tmp.parent = transform.parent;
            tmp = Instantiate(itemCanBeSpawn[_indexRandomSpawn], transform.position, transform.rotation).transform;
            tmp.parent = transform.parent;
        }
        Destroy(gameObject, 0.1f);
        return true;
    }
}
