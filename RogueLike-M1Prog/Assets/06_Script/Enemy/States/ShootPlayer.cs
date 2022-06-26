using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "States/ShootPlayer")]
public class ShootPlayer : AState
{
    [Header("Timer")]
    float latency = 1;
    float time;
    bool canShoot;

    public override void StartState(AMainData mainData)
    {
        base.StartState(mainData);
    }

    public override void ExecuteState()
    {
        if (Player.Instance && time < latency)
        {
            time += Time.deltaTime;
            if (time >= latency)
            {
                time = 0;
                latency = Random.Range(0.2f, 0.6f);

                RaycastHit hit;
                Vector3 dir = (Player.Instance.transform.position - _mainData.transform.position).normalized;
                if (Physics.Raycast(_mainData.transform.position, dir, out hit, 50))
                {
                    if (hit.collider.CompareTag(Constants.TagPlayer))
                        canShoot = true;
                    else
                        canShoot = false;
                }
            }
        }

        if (canShoot)
            ShootOnPlayer();
    }

    public override void EndState()
    {

    }

    void ShootOnPlayer()
    {
        _mainData.WeaponManager.Shoot(new Vector3(_mainData.WeaponManager.weapon._canon.forward.x, 0, _mainData.WeaponManager.weapon._canon.forward.z), 1f);
    }
}
