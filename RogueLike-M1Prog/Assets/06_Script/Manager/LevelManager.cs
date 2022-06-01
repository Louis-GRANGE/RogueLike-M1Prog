using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : Singleton<LevelManager>
{
    //Event when map generation end
    public delegate void OnMapEndGeneration();
    public OnMapEndGeneration Callback_OnMapEndGeneration;

    public bool MapGenerationEnd;

    //Event when player finish room
    public delegate void OnRoomFinish(Room room);
    public OnRoomFinish Callback_OnRoomFinish;

    //Event when end of level
    public delegate void OnEndLevel(Room room);
    public OnEndLevel Callback_OnEndLevel;

    public List<Room> Rooms;

    public MapGenerator RefMapGenerator;
    public Portal RefPortal;


    private void Start()
    {
        Callback_OnRoomFinish += RoomFinish;
        Callback_OnMapEndGeneration += MapEndGeneration;
    }

    private void OnDisable()
    {
        Callback_OnRoomFinish -= RoomFinish;
        Callback_OnMapEndGeneration -= MapEndGeneration;
    }

    public void RoomFinish(Room roomFinish)
    {
        bool IsEnd = true;
        foreach (Room room in Rooms)
        {
            if (!room.IsCompleted)
            {
                IsEnd = false;
                break;
            }
        }

        if (IsEnd)
        {
            Debug.Log("IS END");
            RefMapGenerator.SpawnPortal(roomFinish);
            Callback_OnEndLevel?.Invoke(roomFinish);
        }
    }

    public void MapEndGeneration()
    {
        MapGenerationEnd = true;
        GameManager.instance.PlayerRef.transform.position = Rooms[0].transform.position + Vector3.up;
    }

    public void LoadNewMap()
    {
        foreach (Room MyRoom in Rooms)
        {
            Destroy(MyRoom.gameObject);
        }
        Rooms.Clear();
        RefMapGenerator.InitMap();
    }
}
