using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/TargetPlayer")]
public class TargetPlayer : AState
{
    public override void StartState(AMainData mainData)
    {
        base.StartState(mainData);
    }

    public override void ExecuteState()
    {
        LookAtPlayer();
    }

    public override void EndState()
    {

    }

    void LookAtPlayer()
    {
        _mainData.transform.LookAt(Player.Instance.transform.position);
    }
}
