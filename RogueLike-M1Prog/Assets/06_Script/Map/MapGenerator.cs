using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public List<Room> PrefabsRoom;

    public List<Room> Map;
    private List<Room> RoomByNbDoors;
    public int StartingRoomWithNbDoor = 1;
    public int MaxXSize = 50;
    public int MaxYSize = 50;
    public int MaxErrorTry = 100;

    public Vector2 PartRoomSize;

    public int NumberRooms;

    private void Start()
    {
        Map = new List<Room>();
        RoomByNbDoors = getRoomByNbDoor(StartingRoomWithNbDoor);

        SpawnRoom(0, 0, RoomByNbDoors[Random.Range(0, RoomByNbDoors.Count)], null);
        InitMap();

        StartCoroutine(BuildNavMesh());
    }

    IEnumerator BuildNavMesh()
    {
        yield return new WaitForSeconds(0.1f);
        GetComponent<NavMeshSurface>().BuildNavMesh();
    }

    void InitMap()
    {
        int errorStop = 0;
        int previousMapCount = 0;
        while (Map.Count < NumberRooms && errorStop < MaxErrorTry)
        {
            if (Map.Count == previousMapCount)
            {
                errorStop++;
            }
            else
            {
                errorStop = 0;
            }
            previousMapCount = Map.Count;
            SpawnRoomsOnDoorOfRoom(getRandomRoomWithNoAllDoorConnected());
        }

        foreach (Room MyRoom in Map)
        {
            MyRoom.ReplaceAllWallWithoutRoom();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            SpawnRoomsOnDoorOfRoom(getRandomRoomWithNoAllDoorConnected());
        }
    }

    Room getRandomRoomWithNoAllDoorConnected()
    {
        List<Room> Rooms = new List<Room>();
        foreach (Room MyRoom in Map)
        {
            if (MyRoom.HaveDoorNotConnect())
            {
                Rooms.Add(MyRoom);
            }
        }
        return Rooms[Random.Range(0, Rooms.Count)];
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

    void SpawnRoom(float PosX, float PosY, Room room, Door DoorConnect)
    {
        DoorDir oppositeDoor;
        Room roomInst;
        if (Map.Count < NumberRooms && Mathf.Abs(PosX) < MaxXSize && Mathf.Abs(PosY) < MaxYSize)
        {
            if (DoorConnect)
            {
                if (!DoorConnect.HaveNextToRoom)
                {
                    oppositeDoor = DoorConnect.GetOppositeDir();
                    roomInst = Instantiate(room, new Vector3(PosX - room.getDoorPosByDirection(oppositeDoor).transform.localPosition.x, 0, PosY - room.getDoorPosByDirection(oppositeDoor).transform.localPosition.z), Quaternion.identity);
                    roomInst.name = PosX + " " + PosY;
                    List<Room> OnHitRoom = roomInst.HaveRoomOnIt();
                    if (OnHitRoom.Count > 0)
                    {
                        Destroy(roomInst.gameObject);
                    }
                    else
                    {
                        Map.Add(roomInst);
                        DoorConnect.HaveNextToRoom = true;
                        roomInst.getDoorPosByDirection(oppositeDoor).HaveNextToRoom = true;
                    }
                }
            }
            else
            {
                roomInst = Instantiate(room, new Vector3(PosX + room.DoorPlacement[0].transform.localPosition.x, 0, PosY + room.DoorPlacement[0].transform.localPosition.z), Quaternion.identity);
                roomInst.name = PosX + " " + PosY;
                Map.Add(roomInst);
            }
        }
    }

    void SpawnRoomsOnDoorOfRoom(Room MyRoom)
    {
        foreach (Door Place in MyRoom.DoorPlacement)
        {
            if (!Place.HaveNextToRoom)
            {
                Room newRoom = getRoomWithDoorDir(Place.GetOppositeDir());
                SpawnRoom(Place.transform.position.x, Place.transform.position.z, newRoom, Place);
            }
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

