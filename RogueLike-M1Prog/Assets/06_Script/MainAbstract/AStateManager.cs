using System.Collections.Generic;
using UnityEngine;


public abstract class AStateManager : MonoBehaviour
{
    protected AMainData ownerMainData;

    [System.Serializable]
    public struct SState
    {
        public string State;
        public AState StateScript;
    }
    public SState[] states;
    
    protected List<SState> _activeStates;
    public List<string> activeStates;

    public System.Action<string> OnStateChange;

    protected void Awake()
    {
        ownerMainData = GetComponent<AMainData>();
        ownerMainData.stateManager = this;
    }

    protected virtual void Start()
    {
        _activeStates = new List<SState>();
        SetActiveState(activeStates);
    }

    protected virtual void Update()
    {
        foreach (SState item in _activeStates)
        {
            item.StateScript.ExecuteState(ownerMainData);
        }
    }

    protected virtual void TriggerEndState(SState triggerSState)
    {
        triggerSState.StateScript.EndState(ownerMainData);
    }

    protected virtual void TriggerStartState(SState triggerSState)
    {
        triggerSState.StateScript.StartState(ownerMainData);
    }

    public bool getStateDataByKey(string eEnemyState, out SState outState)
    {
        foreach (SState sState in states)
        {
            if (sState.State == eEnemyState)
            {
                outState = sState;
                return true;
            }
        }
        outState = states[0];
        return false;
    }

    public void SetActiveState(List<string> state)
    {
        activeStates = state;
        foreach (string item in activeStates)
        {
            SState sStateToAdd;
            getStateDataByKey(item, out sStateToAdd);
            _activeStates.Add(sStateToAdd);
            sStateToAdd.StateScript.StartState(ownerMainData);
        }
    }
    public bool RemoveState(string stateToRemove)
    {
        SState sStateToRemove;
        bool foundState = getStateDataByKey(stateToRemove, out sStateToRemove);
        if (foundState && _activeStates.Contains(sStateToRemove))
        {
            _activeStates.Remove(sStateToRemove);
            sStateToRemove.StateScript.EndState(ownerMainData);
            return true;
        }
        return false;
    }
    public void RemoveStates(string[] allStateToRemove)
    {
        foreach (string stateToRemove in allStateToRemove)
        {
            RemoveState(stateToRemove);
        }
    }
    public bool AddState(string stateToAdd)
    {
        if (!activeStates.Contains(stateToAdd))
        {
            SState sStateToAdd;
            getStateDataByKey(stateToAdd, out sStateToAdd);
            _activeStates.Add(sStateToAdd);
            activeStates.Add(stateToAdd);
            sStateToAdd.StateScript.StartState(ownerMainData);
            return true;
        }
        return false;
    }
    public void AddStates(string[] allStateToAdd)
    {
        foreach (string stateToAdd in allStateToAdd)
        {
            AddState(stateToAdd);
        }
    }
    public void ClearStates()
    {
        for (int i = _activeStates.Count - 1; i > 0; i--)
        {
            _activeStates[i].StateScript.EndState(ownerMainData);
            _activeStates.RemoveAt(i);
        }
    }

}
