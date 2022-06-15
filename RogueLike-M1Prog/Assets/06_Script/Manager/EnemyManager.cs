using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public List<Enemy> Enemies;

    private void Start()
    {
        GameManager.instance.EnemyManagerRef = this;
    }
}
