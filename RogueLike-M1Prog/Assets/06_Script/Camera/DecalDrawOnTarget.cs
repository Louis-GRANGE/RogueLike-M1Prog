using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DecalDrawOnTarget : MonoBehaviour
{
    public DecalProjector URPDecalProjector;
    Player _player;
    private void Start()
    {
        _player = Player.Instance;
    }

    private void Update()
    {
        URPDecalProjector.transform.position = _player.playerWeaponManager.TargetShootPos + Vector3.up;
        Debug.Log("HaveTarget: " + _player.playerWeaponManager.HaveTarget);
        if (_player.playerWeaponManager.HaveTarget)
            URPDecalProjector.material.SetColor("_Tint", new Color(1, 0, 0)); // Set Red Shoot on Target
        else
            URPDecalProjector.material.SetColor("_Tint", new Color(0, 1, 0)); // Set Green Safe shoot
    }
}
