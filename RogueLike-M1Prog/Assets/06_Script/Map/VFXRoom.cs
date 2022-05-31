using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXRoom : MonoBehaviour
{
    Room RoomRef;
    ParticleSystem _particleSystem;
    void Start()
    {
        RoomRef = GetComponentInParent<Room>();
        _particleSystem = GetComponent<ParticleSystem>();

        RoomRef.OnPlayerEnter += RemoveFogRoom;
    }

    private void OnDisable()
    {
        if(RoomRef)
            RoomRef.OnPlayerEnter -= RemoveFogRoom;
    }

    public void RemoveFogRoom()
    {
        _particleSystem.loop = false;
        _particleSystem.playbackSpeed = 10;
        Destroy(gameObject, 1.0f);
    }
}
