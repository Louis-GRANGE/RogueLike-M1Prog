using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[System.Serializable]
public abstract class AState : MonoBehaviour
{
    protected AMainData _mainData;
    public virtual void StartState(AMainData mainData)
    {
        _mainData = mainData;
    }

    public virtual void ExecuteState()
    {

    }

    public virtual void EndState()
    {

    }
}
