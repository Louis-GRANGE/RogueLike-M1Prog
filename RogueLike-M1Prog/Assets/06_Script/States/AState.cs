using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[System.Serializable]
public abstract class AState : ScriptableObject
{
    public virtual void StartState(AMainData mainData)
    {

    }

    public virtual void ExecuteState(AMainData mainData)
    {

    }

    public virtual void EndState(AMainData mainData)
    {

    }
}
