using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXRoom : MonoBehaviour
{
    Room RoomRef;
    ParticleSystem _particleSystem;
    ParticleSystem.MainModule _particleSystemMain;

    void Start()
    {
        RoomRef = GetComponentInParent<Room>();
        _particleSystem = GetComponent<ParticleSystem>();
        _particleSystemMain = _particleSystem.main;

        RoomRef.OnPlayerEnter += RemoveFogRoom;
    }

    private void OnDestroy()
    {
        if (RoomRef)
        {
            RoomRef.OnPlayerEnter -= RemoveFogRoom;
        }
    }

    public void RemoveFogRoom()
    {
        _particleSystemMain.loop = false;
        _particleSystemMain.simulationSpeed = 10;
        Destroy(gameObject, 1.0f);
    }
}
