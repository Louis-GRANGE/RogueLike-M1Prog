using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{
    private static T _instance;
    public static T instance
    {
        get
        {
            if(_instance == null)
            {
                //Debug.LogError("NEED INSTANCE");
                _instance = FindObjectOfType<T>();
                //if(_instance == null)
                //{
                //    GameObject newGo = new GameObject();
                //    _instance = newGo.AddComponent<T>();
                //}
            }
            return _instance;
        }
    }

    protected virtual void Awake()
    {
        _instance = this as T;
    }
}
