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

    public override void TakeDamage(int damage, GameObject Sender)
    {
        base.TakeDamage(damage, Sender);



        damageTextPool.RequestDamageText(transform.position, damage);
        
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
            Instantiate(prefabBrakedCrate, transform.position, transform.rotation);
            Instantiate(itemCanBeSpawn[_indexRandomSpawn], transform.position, transform.rotation);
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
            Instantiate(prefabBrakedCrate, transform.position, transform.rotation);
            Instantiate(itemCanBeSpawn[_indexRandomSpawn], transform.position, transform.rotation);
        }
        Destroy(gameObject, 0.1f);
        return true;
    }
}
