using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovementKamikazeEnemy : AMovementEnemy
{
    public override void Anim()
    {
        float moveDirection = NavMeshAgent.velocity.magnitude;
        _animator.SetFloat("Running", moveDirection);
    }
}
