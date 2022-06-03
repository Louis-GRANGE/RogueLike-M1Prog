using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementEnemy : AMovementManager
{
    [Header("Internal Components")]
    Animator _animator;

    public override void Awake()
    {
        base.Awake();
        _animator = transform.GetChild(0).GetComponent<Animator>();
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
