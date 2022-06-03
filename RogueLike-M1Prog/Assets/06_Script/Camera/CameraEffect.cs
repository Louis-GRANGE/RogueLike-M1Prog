using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEffect : MonoBehaviour
{
    [HideInInspector] public static CameraEffect Instance;

    Animator _animator;

    private void Awake()
    {
        if (!Instance)
            Instance = this;
        else
            Destroy(gameObject);

        _animator = transform.GetChild(0).GetComponent<Animator>();
    }

    public void Explosion() => _animator.SetTrigger("Explosion");
}
