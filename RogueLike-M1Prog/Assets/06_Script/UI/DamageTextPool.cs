using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTextPool : MonoBehaviour
{
    [HideInInspector] public static DamageTextPool Instance;

    public Transform alterPool;

    private void Awake()
    {
        if (!Instance)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        for (int i = 0; i < 50; i++)
        {
            GameObject text = Instantiate(Resources.Load<GameObject>("UI/DamageText"), transform);
            text.GetComponent<DamageEffect>().pool = transform;
        }
    }

    public void RequestDamageText(Vector3 worldPos, int damage)
    {
        Transform child = transform.GetChild(0);
        child.SetParent(alterPool);
        child.GetComponent<DamageEffect>().Activation(worldPos, damage);
    }
}
