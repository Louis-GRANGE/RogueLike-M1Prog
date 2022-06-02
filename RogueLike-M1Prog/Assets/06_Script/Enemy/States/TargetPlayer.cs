using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/TargetPlayer")]
public class TargetPlayer : AState
{
    public override void StartState(AMainData mainData)
    {
        base.StartState(mainData);
        Debug.Log("[INIT] TargetPlayer");
    }

    public override void ExecuteState(AMainData mainData)
    {
        LookAtPlayer(mainData);
    }

    public override void EndState(AMainData mainData)
    {

    }

    void LookAtPlayer(AMainData mainData)
    {
        mainData.transform.LookAt(Player.Instance.transform.position);
    }
}
