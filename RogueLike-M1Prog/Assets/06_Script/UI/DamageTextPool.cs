using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTextPool : Singleton<DamageTextPool>
{
    public Transform alterPool;

    private void Start()
    {
        for (int i = 0; i < 50; i++)
        {
            GameObject text = Instantiate(Resources.Load<GameObject>("UI/PopUpText"), transform);
            text.GetComponent<DamageEffect>().pool = transform;
        }
    }

    public void RequestDamageText(Vector3 worldPos, int damage)
    {
        if (transform.childCount <= 0)
            return;
        Transform child = transform.GetChild(0);
        child.SetParent(alterPool);
        child.GetComponent<DamageEffect>().Activation(worldPos, damage.ToString(), Color.red);
    }

    public void RequestHealText(Vector3 worldPos, int heal)
    {
        Transform child = transform.GetChild(0);
        child.SetParent(alterPool);
        child.GetComponent<DamageEffect>().Activation(worldPos, heal.ToString(), Color.green);
    }

    public void RequestMunitionText(Vector3 worldPos, int value)
    {
        Transform child = transform.GetChild(0);
        child.SetParent(alterPool);
        child.GetComponent<DamageEffect>().Activation(worldPos, "+" + value, Color.green);
    }
}
