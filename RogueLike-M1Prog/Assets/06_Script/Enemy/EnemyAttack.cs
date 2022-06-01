using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : State
{
    MainData enemy;

    public override void StartState(MainData mainData)
    {
        mainData = GetComponent<Enemy>();
    }

    public override void ExecuteState()
    {

    }

    public override void EndState()
    {

    }
}
