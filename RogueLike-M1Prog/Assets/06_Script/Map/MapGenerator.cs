using GD.MinMaxSlider;
using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public List<Room> PrefabsRoom;

    public Portal PrefabPortal;

    public List<Room> Rooms;

    private List<Room> RoomByNbDoors;

    [MinMaxSlider(0, 100)]
    public Vector2Int NumberMaxRoom;
    

    public int StartingRoomWithNbDoor = 1;
    private int RoomToSpawn;
    public int MaxErrorTry = 100;

    private void Start()
    {
        LevelManager.instance.RefMapGenerator = this;

        InitMap();
    }

    IEnumerator EndOfMapGeneration()
    {
        yield return new WaitForSeconds(0.1f);

        // Check if there is amount of map enter slider
        if (Rooms.Count < RoomToSpawn)
        {
            Debug.Log("RESET");
            foreach (Room MyRoom in Rooms)
            {
                Destroy(MyRoom.gameObject);
            }
            Rooms.Clear();
            InitMap();
        }
        else
        {
            Rooms[0].IsCompleted = true;
            Rooms[0].ActiveMinimap(true);
            foreach (Room room in Rooms)
            {
                room.SpawnAllOfRoomEnnemies();
            }
            LevelManager.instance.Rooms = new List<Room>(Rooms);
            Rooms[0].IsCompleted = true; // Send to LevelManager the end of first map
            Rooms.Clear();

            GetComponent<NavMeshSurface>().BuildNavMesh();
            //Destroy(this, 0.1f);
            LevelManager.instance.Callback_OnMapEndGeneration.Invoke(); // Send to LevelManager the end of generation
        }
    }

    public void SpawnPortal(Room roomFinish)
    {
        Debug.Log("SPAWN PORTAL");
        Instantiate(PrefabPortal, roomFinish.transform.position + Vector3.up, Quaternion.identity);
    }

    public void InitMap()
    {
        RoomToSpawn = Random.Range(NumberMaxRoom.x, NumberMaxRoom.y) + GameManager.instance.Difficulty;
        Rooms = new List<Room>();

        RoomByNbDoors = getRoomByNbDoor(StartingRoomWithNbDoor);

        SpawnRoom(0, 0, RoomByNbDoors[Random.Range(0, RoomByNbDoors.Count)], null, null);

        int errorStop = 0;
        int previousMapCount = 0;
        while (Rooms.Count < RoomToSpawn && errorStop < MaxErrorTry)
        {
            if (Rooms.Count == previousMapCount)
            {
                errorStop++;
            }
            else
            {
                errorStop = 0;
            }
            previousMapCount = Rooms.Count;
            SpawnRoomsOnDoorOfRoom(getRandomRoomWithNoAllDoorConnected());
        }

        foreach (Room MyRoom in Rooms)
        {
            MyRoom.ReplaceAllWallWithoutRoom();
        }

        StartCoroutine(EndOfMapGeneration());
    }

    Room getRandomRoomWithNoAllDoorConnected()
    {
        List<Room> RoomsWithFreeDoor = new List<Room>();
        foreach (Room MyRoom in Rooms)
        {
            if (MyRoom.HaveDoorNotConnect())
            {
                RoomsWithFreeDoor.Add(MyRoom);
            }
        }
        return RoomsWithFreeDoor[Random.Range(0, RoomsWithFreeDoor.Count)];
    }

    List<Room> getRoomByNbDoor(int NbDoor)
    {
        List<Room> RoomOneDoor = new List<Room>();

        foreach (Room RoomInList in PrefabsRoom)
        {
            if (RoomInList.DoorPlacement.Count == NbDoor)
            {
                RoomOneDoor.Add(RoomInList);
            }
        }
        return RoomOneDoor;
    }

    void SpawnRoom(float PosX, float PosY, Room room, Door DoorConnect, Room Neighboor)
    {
        DoorDir oppositeDoor;
        if (Rooms.Count < RoomToSpawn)
        {
            Room roomInst = null;
            if (DoorConnect)
            {
                if (!DoorConnect.HaveNextToRoom)
                {
                    oppositeDoor = DoorConnect.GetOppositeDir();
                    roomInst = Instantiate(room, new Vector3(PosX - room.getDoorPosByDirection(oppositeDoor).transform.localPosition.x, 0, PosY - room.getDoorPosByDirection(oppositeDoor).transform.localPosition.z), Quaternion.identity);
                    roomInst.name = PosX + " " + PosY;
                    roomInst.transform.parent = gameObject.transform;
                    List<Room> OnHitRoom = roomInst.HaveRoomOnIt();
                    if (OnHitRoom.Count > 0)
                    {
                        Destroy(roomInst.gameObject);
                    }
                    else
                    {
                        Rooms.Add(roomInst);
                        DoorConnect.HaveNextToRoom = true;
                        roomInst.getDoorPosByDirection(oppositeDoor).HaveNextToRoom = true;
                        roomInst.NeighboorsRooms.Add(Neighboor);
                        Neighboor.NeighboorsRooms.Add(roomInst);
                    }
                }
            }
            else
            {
                roomInst = Instantiate(room, new Vector3(PosX + room.DoorPlacement[0].transform.localPosition.x, 0, PosY + room.DoorPlacement[0].transform.localPosition.z), Quaternion.identity);
                roomInst.transform.parent = gameObject.transform;
                roomInst.name = PosX + " " + PosY;
                Rooms.Add(roomInst);
            }
        }
    }

    void SpawnRoomsOnDoorOfRoom(Room MyRoom)
    {
        List<Door> FreeDoor = new List<Door>();
        foreach (Door Place in MyRoom.DoorPlacement)
        {
            if (!Place.HaveNextToRoom)
            {
                FreeDoor.Add(Place);
            }
        }
        if (FreeDoor.Count > 0)
        {
            Door RandomDoor = FreeDoor[Random.Range(0, FreeDoor.Count)];
            Room newRoom = getRoomWithDoorDir(RandomDoor.GetOppositeDir());
            SpawnRoom(RandomDoor.transform.position.x, RandomDoor.transform.position.z, newRoom, RandomDoor, MyRoom);
        }
    }

    Room getRoomWithDoorDir(DoorDir dir)
    {
        List<Room> Rooms = new List<Room>();
        foreach (Room room in PrefabsRoom)
        {
            if (room.getDoorPosByDirection(dir))
            {
                Rooms.Add(room);
            }
        }
        return Rooms[Random.Range(0, Rooms.Count)];
    }
}

