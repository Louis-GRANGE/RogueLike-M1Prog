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
        foreach (Room room in RefMapGenerator.Rooms)
        {
            if (!room.IsCompleted)
            {
                IsEnd = false;
                break;
            }
        }

        if (IsEnd)
        {
            RefMapGenerator.SpawnPortal(roomFinish);
            Callback_OnEndLevel?.Invoke(roomFinish);
        }

        SaveManager.instance.SaveAll();
    }

    public void MapEndGeneration()
    {
        MapGenerationEnd = true;

        foreach (Player player in GameManager.instance.Players)
        {
            player.transform.position = RefMapGenerator.Rooms[0].transform.position + Vector3.up;
        }

        //GameManager.instance.PlayerRef.transform.position = RefMapGenerator.Rooms[0].transform.position + Vector3.up;
    }

    public void LoadNewMap()
    {
        foreach (Room MyRoom in RefMapGenerator.Rooms)
        {
            Destroy(MyRoom.gameObject);
        }
        RefMapGenerator.Rooms.Clear();
        RefMapGenerator.InitMap();
    }
}
