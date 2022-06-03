using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "States/ShootPlayer")]
public class ShootPlayer : AState
{
    public override void StartState(AMainData mainData)
    {
        base.StartState(mainData);
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
