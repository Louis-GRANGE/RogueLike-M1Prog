using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Vector3 _offset = new Vector3(-5, -8, 5);
    Player _player;

    private void Start()
    {
        _player = Player.Instance;

        //_offset = _player.transform.position - transform.position;
    }

    private void FixedUpdate()
    {
        if (_player)
        {
            Vector3 _direction = (_player.transform.position - transform.position) - _offset;
            transform.position += _direction / 10;
        }
    }
}
