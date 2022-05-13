using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Room))]
public class InitListDoorOfRoom : Editor
{
    public override void OnInspectorGUI()
    {
        Room MyRoom = (Room)target;
        DrawDefaultInspector();

        if (GUILayout.Button("Init List Door"))
        {
            MyRoom.InitArrayDoor();
        }
    }
}