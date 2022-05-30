using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserEffect : MonoBehaviour
{
    [Header("Pool system")]
    Transform _pool;
    public float returnLatency = 1;
    float _returnTime;

    [Header("Components")]
    LineRenderer _lineRenderer;

    public void Initiate(Transform newPool)
    {
        _pool = newPool;
        _lineRenderer = GetComponent<LineRenderer>();
        gameObject.SetActive(false);
    }

    public void DrawLine(Vector3 origin, Vector3 target)
    {
        transform.parent = null;

        Vector3[] points = new Vector3[2];
        points[0] = origin;
        points[1] = target;

        _lineRenderer.SetPositions(points);

        gameObject.SetActive(true);
    }

    private void Update()
    {
        _returnTime += Time.deltaTime;
        if(_returnTime > returnLatency)
        {
            transform.parent = _pool;
            _returnTime = 0;
            gameObject.SetActive(false);
        }
    }
}
