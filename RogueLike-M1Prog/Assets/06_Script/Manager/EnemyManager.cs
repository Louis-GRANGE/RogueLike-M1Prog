using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public List<Enemy> Enemies;
    public bool InCombat = false;

    private void Start()
    {
        GameManager.instance.EnemyManagerRef = this;
    }

    public void StartCombat(bool doIt)
    {
        InCombat = doIt;
        MusicManager.Instance.ChangeSituation(doIt);
    }
}
