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
    public bool HaveNextToRoom = false;
    public DoorDir DoorDirection;

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
}
