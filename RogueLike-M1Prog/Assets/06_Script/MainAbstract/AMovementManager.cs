using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public abstract class AMovementManager : MonoBehaviour
{
    protected AMainData ownerMainData;

    [Header("Internal Components")]
    protected Animator _animator;

    protected virtual void Awake()
    {
        ownerMainData = GetComponent<AMainData>();
        ownerMainData.MovementManager = this;

        _animator = transform.GetChild(0).GetComponent<Animator>();
    }
}
