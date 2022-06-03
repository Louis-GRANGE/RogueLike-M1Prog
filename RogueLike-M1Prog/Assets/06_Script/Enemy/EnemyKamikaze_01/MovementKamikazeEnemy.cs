using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementKamikazeEnemy : AMovementManager
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
        float moveDirection = NavMeshAgent.velocity.magnitude;
        _animator.SetFloat("Running", moveDirection);
    }
}
