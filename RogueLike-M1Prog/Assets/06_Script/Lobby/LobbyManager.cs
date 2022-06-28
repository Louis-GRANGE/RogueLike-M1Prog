using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LobbyManager : Singleton<LobbyManager>
{
    public List<Transform> SpawnPoint;

    public Transform getSpawnPointAndRemoveIt()
    {
        if (SpawnPoint.Count > 0)
        {
            int randomindex = SpawnPoint.Count -1;// Random.Range(0, SpawnPoint.Count);
            Transform transform = SpawnPoint[randomindex];
            SpawnPoint.RemoveAt(randomindex);
            Debug.Log("RemoveIndex: " + randomindex);
            return transform;
        }
        Debug.LogError("[LobbyManager] No More Spawn point for player");
        return null;
    }
}
