using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Minimap : MonoBehaviour
{
    Camera _camera;
    bool _isOpen = false;
    float InitSize;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
        InitSize = _camera.orthographicSize;
    }
    public void ToggleMinimap()
    {
        _isOpen = !_isOpen;
        if (_isOpen)
        {
            _camera.rect = new Rect(0, 0.15f, 1, 1);
            _camera.orthographicSize = 100;
        }
        else
        {
            _camera.rect = new Rect(0.8f, 0.75f, 1, 1);
            _camera.orthographicSize = InitSize;
        }
    }
}
