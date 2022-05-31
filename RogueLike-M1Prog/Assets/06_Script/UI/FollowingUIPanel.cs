using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingUIPanel : MonoBehaviour
{
    [HideInInspector] public static FollowingUIPanel Instance;

    private void Awake()
    {
        if (!Instance)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void UpdateValues()
    {

    }
}
