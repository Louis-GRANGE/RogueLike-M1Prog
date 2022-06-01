using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    MainData mainData;
    public virtual void StartState(MainData mainData)
    {
        this.mainData = mainData;
    }

    public virtual void ExecuteState()
    {

    }

    public virtual void EndState()
    {

    }
}
