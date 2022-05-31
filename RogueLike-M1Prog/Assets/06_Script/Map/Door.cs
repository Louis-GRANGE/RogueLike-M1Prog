using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DoorDir
{
    North,
    South,
    West,
    East,
}

public class Door : MonoBehaviour
{
    [HideInInspector]
    public bool HaveNextToRoom = false;

    public DoorDir DoorDirection;
    public GameObject GoDoor;
    private Vector3 _closePos;
    private Vector3 _openPos;
    private bool _isOpen = false;
    public bool CanOpenDoor = true;
    private float speed = 15;

    private void Awake()
    {
        _closePos = GoDoor.transform.position;
        _openPos = _closePos + Vector3.up * 3f;
    }

    /*private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            OpenDoor(!_isOpen);
        }
    }*/

    public DoorDir GetOppositeDir()
    {
        switch (DoorDirection)
        {
            case DoorDir.North:
                return DoorDir.South;
            case DoorDir.South:
                return DoorDir.North;
            case DoorDir.West:
                return DoorDir.East;
            case DoorDir.East:
                return DoorDir.West;
        }
        return DoorDir.North;
    }

    public void OpenDoor(bool IsOpen)
    {
        StopAllCoroutines();
        _isOpen = IsOpen;
        StartCoroutine(IEOpenDoor(IsOpen));
    }

    IEnumerator IEOpenDoor(bool IsOpen)
    {
        if (IsOpen)
        {
            while (GoDoor.transform.position.y < _openPos.y)
            {
                GoDoor.transform.position = GoDoor.transform.position + Vector3.up * Time.deltaTime * speed;
                yield return new WaitForSeconds(0.01f);
            }
        }
        else
        {
            while (GoDoor.transform.position.y > _closePos.y)
            {
                GoDoor.transform.position = GoDoor.transform.position + Vector3.up * Time.deltaTime * -speed;
                yield return new WaitForSeconds(0.01f);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && CanOpenDoor)
        {
            OpenDoor(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && CanOpenDoor)
        {
            OpenDoor(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OpenDoor(false);
        }
    }
}
