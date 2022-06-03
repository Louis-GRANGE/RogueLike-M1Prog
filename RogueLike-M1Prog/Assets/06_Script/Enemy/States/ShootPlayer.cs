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

    public override void ExecuteState(AMainData mainData)
    {
        ShootOnPlayer(mainData);
    }

    public override void EndState(AMainData mainData)
    {

    }

    void ShootOnPlayer(AMainData mainData)
    {
        mainData.WeaponManager.Shoot(new Vector3(mainData.WeaponManager._canon.forward.x, 0, mainData.WeaponManager._canon.forward.z), 1f);
    }
}
