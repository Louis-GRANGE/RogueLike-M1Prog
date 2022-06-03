using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementEnemy : AMovementManager
{
    public override void Awake()
    {
        base.Awake();
    }

    private void Update()
    {
        Anim();
    }

    public void Anim()
    {
        Vector3 pointDirection = transform.forward;
        Vector3 moveDirection = NavMeshAgent.velocity;
        Vector3 direction = Quaternion.Euler(0, Vector3.Angle(pointDirection, moveDirection), 0) * moveDirection * moveDirection.magnitude;

        _animator.SetFloat("MoveX", direction.normalized.x);
        _animator.SetFloat("MoveY", direction.normalized.z);
    }
}
