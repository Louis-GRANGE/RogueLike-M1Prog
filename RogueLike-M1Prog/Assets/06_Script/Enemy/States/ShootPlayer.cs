using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShootPlayer : AState
{
    Transform PlayerPos;
    public override void StartState(AMainData mainData)
    {
        base.StartState(mainData);
        PlayerPos = Player.Instance.transform;
        Debug.Log("[INIT] ShootPlayer");
    }

    public override void ExecuteState()
    {
        ShootOnPlayer();
    }

    public override void EndState()
    {

    }

    void ShootOnPlayer()
    {
        _mainData.WeaponManager.Shoot(_mainData.WeaponManager._canon.forward);
    }
}
