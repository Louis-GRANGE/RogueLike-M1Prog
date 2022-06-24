using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DecalDrawOnTarget : MonoBehaviour
{
    public DecalProjector URPDecalProjector;
    PlayerWeaponManager _playerShoot;
    private void Start()
    {
        _playerShoot = Player.Instance.playerWeaponManager;
    }

    private void Update()
    {
        URPDecalProjector.transform.position = _playerShoot.TargetShootPos + Vector3.up;
        if (_playerShoot.HaveTarget)
            URPDecalProjector.material.SetColor("_Tint", new Color(1, 0, 0)); // Set Red Shoot on Target
        else
            URPDecalProjector.material.SetColor("_Tint", new Color(0, 1, 0)); // Set Green Safe shoot
    }
}
