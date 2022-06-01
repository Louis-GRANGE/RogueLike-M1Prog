using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    [Header("External Components")]
    Rigidbody _mainRigidbody;

    [Header("Ragdoll Timer")]
    float _latency = 5;
    float _time;
    bool _movable = true;

    private void Awake()
    {
        _mainRigidbody = transform.GetChild(0).GetChild(0).GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (_movable)
        {
            _time += Time.deltaTime;
            if(_time >= _latency && _mainRigidbody.velocity.magnitude <= 0.01f)
            {
                transform.GetChild(0).gameObject.SetActive(false);
                _movable = false;
            }
        }
    }
}
