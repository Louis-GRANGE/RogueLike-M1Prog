using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Room : AMinimap
{
    public List<Door> DoorPlacement;
    public List<Room> NeighboorsRooms;
    public List<Enemy> Enemies;

    public System.Action OnPlayerEnter;

    public int SizeRoom;
    private List<BoxCollider> RoomArea;
    public GameObject PrefabWallToReplace;

    public bool IsCompleted;
    private bool _haveSpawnEnnemies;

    private void Awake()
    {
        RoomArea = GetComponents<BoxCollider>().ToList();
        if (SizeRoom == 0)
        {
            IsCompleted = true;
        }
    }

    public void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            if(Enemies.Count > 0)
            {
                Destroy(Enemies[Random.Range(0, Enemies.Count)].gameObject);
            }
            else
            {
                Enemies.Clear();
            }
        }
    }

    public Door getDoorPosByDirection(DoorDir direction)
    {
        foreach (Door door in DoorPlacement)
        {
            if (door.DoorDirection == direction)
            {
                return door;
            }
        }
        return null;
    }

    public void InitArrayDoor()
    {
        DoorPlacement = GetComponentsInChildren<Door>().ToList();
    }

    public List<Room> HaveRoomOnIt()
    {
        List<Room> RoomHit = new List<Room>();
        foreach (Collider col in RoomArea)
        {
           /* RaycastHit hit; //retourne lui même donc impossible
            if (Physics.BoxCast(col.bounds.center, col.bounds.size / 2, -Vector3.up, out hit, transform.rotation, 200) && hit.transform.GetComponentInParent<Room>() != this)
            {
                Debug.DrawRay(hit.point, hit.normal * 10, Color.red, 10000);
                Debug.Log("Sender: " + gameObject.name + " target:" + hit.transform.GetComponentInParent<Room>().name);
                return hit.transform.GetComponentInParent<Room>();
            }*/
            RaycastHit[] hits = Physics.BoxCastAll(col.bounds.center, col.bounds.size / 2, -Vector3.up, transform.rotation, 200);
            //Debug.DrawRay(col.bounds.center, Vector3.up * 1000, Color.yellow, 100);
            foreach (RaycastHit hit in hits)
            {
                if (hit.transform.GetComponentInParent<Room>() != this)
                {
                    //Debug.DrawRay(hit.point, hit.normal * 10, Color.red, 10000);
                    RoomHit.Add(hit.transform.GetComponentInParent<Room>());
                }
            }
        }
        return RoomHit;
    }

    public bool HaveDoorNotConnect()
    {
        foreach (Door _door in DoorPlacement)
        {
            if (!_door.HaveNextToRoom)
            {
                return true;
            }
        }
        return false;
    }

    public void ReplaceAllWallWithoutRoom()
    {
        foreach (Door _door in DoorPlacement)
        {
            if (!_door.HaveNextToRoom)
            {
                if (_door.DoorDirection == DoorDir.West)
                {
                    GameObject goWall = Instantiate(PrefabWallToReplace, _door.transform.position - Vector3.right * 5, _door.transform.rotation * Quaternion.Euler(0, 180, 0));
                    goWall.transform.SetParent(transform);
                }
                else if (_door.DoorDirection == DoorDir.North)
                {
                    GameObject goWall = Instantiate(PrefabWallToReplace, _door.transform.position - Vector3.forward * -5, _door.transform.rotation * Quaternion.Euler(0, 180, 0));
                    goWall.transform.SetParent(transform);
                }
                else
                {
                    GameObject goWall = Instantiate(PrefabWallToReplace, _door.transform.position, _door.transform.rotation);
                    goWall.transform.SetParent(transform);
                }
                Destroy(_door.gameObject);
            }
        }
    }

    private Vector3 getRandomPointInRoom()
    {
        BoxCollider RandomPartOfRoom = RoomArea[Random.Range(0, RoomArea.Count - 1)];
        return Tools.GetRandomPointInsideCollider(RandomPartOfRoom);
    }

    private void SpawnAllOfRoomEnnemies()
    {
        int NbToSpawn = GameManager.instance.SpawnerRef.SOSpawner.GetRandomNbOfEnnemies() * SizeRoom;
        for (int i = 0; i < NbToSpawn; i++)
        {
            Enemy AISpawned = GameManager.instance.SpawnerRef.SpawnRandomAIOnPos(getRandomPointInRoom());
            AISpawned.RefInRoom = this;
            AISpawned.AIIndex = i;
            Enemies.Add(AISpawned);
            AISpawned.transform.parent = gameObject.transform;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnPlayerEnter?.Invoke();
            if (!IsActiveMinimap)
            {
                ActiveMinimap(true);
            }
            if (!_haveSpawnEnnemies && LevelManager.instance.MapGenerationEnd && !IsCompleted)
            {
                if (SizeRoom != 0)
                {
                    SpawnAllOfRoomEnnemies();
                    _haveSpawnEnnemies = true;
                    foreach (Door door in DoorPlacement)
                    {
                        door.CanOpenDoor = false;
                    }
                }
                else
                {
                    IsCompleted = true;
                }
            }

        }
    }

    public void SendAIDied(Enemy enemyIndex)
    {
        Enemies.Remove(enemyIndex);
        if (Enemies.Count == 0 && !IsCompleted)
        {
            IsCompleted = true;
            foreach (Door item in DoorPlacement)
            {
                item.CanOpenDoor = true;
            }
            LevelManager.instance.Callback_OnRoomFinish?.Invoke(this);
        }
    }
}
