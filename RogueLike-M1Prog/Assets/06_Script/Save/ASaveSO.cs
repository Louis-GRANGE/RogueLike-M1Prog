using System.IO;
using UnityEngine;

public class ASaveSO : ScriptableObject
{
    public string fileName = "Save";

    public void Save()
    {
        var saved = JsonUtility.ToJson(this);
        string path = Application.streamingAssetsPath + "/" + fileName;
        File.WriteAllText(path, saved);
    }

    public void Load()
    {
        string path = Application.streamingAssetsPath + "/" + fileName;
        string thejson = File.ReadAllText(path);
        JsonUtility.FromJsonOverwrite(thejson, this);
    }
}
