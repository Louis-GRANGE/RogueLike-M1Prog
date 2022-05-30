using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffect : MonoBehaviour
{
    [Header("Pool system")]
    Transform _pool;
    public float returnLatency = 1;
    float _returnTime;

    [Header("Components")]
    ParticleSystem _particleSystem;

    public void Initiate(Transform newPool)
    {
        _pool = newPool;
        _particleSystem = GetComponent<ParticleSystem>();
        gameObject.SetActive(false);
    }

    public void DrawParticle(Vector3 newPosition)
    {
        transform.parent = null;

        transform.position = newPosition;

        gameObject.SetActive(true);
        _particleSystem.Play();
    }

    private void Update()
    {
        _returnTime += Time.deltaTime;
        if (_returnTime > returnLatency)
        {
            transform.parent = _pool;
            _returnTime = 0;
            gameObject.SetActive(false);
        }
    }
}
