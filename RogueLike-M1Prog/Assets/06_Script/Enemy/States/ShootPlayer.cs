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
        _mainData.WeaponManager.Shoot(new Vector3(_mainData.WeaponManager._canon.forward.x, 0, _mainData.WeaponManager._canon.forward.z), 1f);
    }
}
