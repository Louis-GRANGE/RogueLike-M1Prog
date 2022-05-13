using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Room : MonoBehaviour
{
    public List<Door> DoorPlacement;
    public bool IsBigRoom;
    private List<BoxCollider> RoomArea;
    public GameObject PrefabWallToReplace;

    private void Awake()
    {
        RoomArea = GetComponents<BoxCollider>().ToList();
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
}
