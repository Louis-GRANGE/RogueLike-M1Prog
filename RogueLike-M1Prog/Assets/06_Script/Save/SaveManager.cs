using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : Singleton<SaveManager>
{
    public List<ASaveSO> Saves;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        //LoadAll();
    }

    public T GetSave<T>() where T : ASaveSO
    {
        foreach (ASaveSO save in Saves)
        {
            if (save is T Tclass)
            {
                return Tclass;
            }
        }
        return null;
    }

    public void SaveAll()
    {
        foreach (ASaveSO save in Saves)
        {
            save.Save();
        }
    }

    public void LoadAll()
    {
        foreach (ASaveSO save in Saves)
        {
            save.Load();
        }
    }
}
