using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DecalDrawOnTarget : MonoBehaviour
{
    public DecalProjector URPDecalProjector;
    //Player _player;
    Player _player;
    private void Start()
    {
        _player = GetComponent<Player>();
        if (_player.playerInputs.GetCurrentDeviceType() != EDeviceType.KeyboardAndMouse)
        {
            Destroy(URPDecalProjector);
            Destroy(this);
        }
        //_player = Player.Instance;
        /*foreach (Player player in GameManager.instance.Players)
        {
            if (player.playerInputs.CurrentDeviceType == EDeviceType.KeyboardAndMouse)
            {
                _player = player;
            }
        }*/
    }

    private void Update()
    {
        URPDecalProjector.transform.position = _player.playerWeaponManager.TargetShootPos + Vector3.up;
        if (_player.playerWeaponManager.HaveTarget)
            URPDecalProjector.material.SetColor("_Tint", new Color(1, 0, 0)); // Set Red Shoot on Target
        else
            URPDecalProjector.material.SetColor("_Tint", new Color(0, 1, 0)); // Set Green Safe shoot
        /*
        URPDecalProjector.transform.position = _player.playerWeaponManager.TargetShootPos + Vector3.up;
        if (_player.playerWeaponManager.HaveTarget)
            URPDecalProjector.material.SetColor("_Tint", new Color(1, 0, 0)); // Set Red Shoot on Target
        else
            URPDecalProjector.material.SetColor("_Tint", new Color(0, 1, 0)); // Set Green Safe shoot*/
    }
}
