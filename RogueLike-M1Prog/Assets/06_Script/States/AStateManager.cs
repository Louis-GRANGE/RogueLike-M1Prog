using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class AStateManager : MonoBehaviour
{
    protected AMainData ownerMainData;
    public string InitState;

    [System.Serializable]
    public struct SState
    {
        public string State;
        public AState StateScript;
    }
    public SState[] states;

    protected SState _currentState;
    //============================================ To add later maybe (Multi state)
    /*public List<SState> _activeStates
    {
        get
        {
            return _activeStates;
        }
        set
        {
            foreach (SState stState in value)
            {
                if (!_activeStates.Contains(stState))
                {
                    SState tmp = stState;
                    TriggerStartState(tmp);
                }
            }
            foreach (SState stState in _activeStates)
            {
                if (!value.Contains(stState))
                {
                    SState tmp = stState;
                    TriggerEndState(tmp);
                }
            }
            _activeStates = value;
        }
    }*/

    //==============================================================================
    public string currentState
    {
        get { return _currentState.State; }
        set
        {
            if (_currentState.State == value) return;
            TriggerEndState(_currentState); //End Of State

            _currentState = getStateDataByState(value);
            TriggerStartState(_currentState); //Start Of State

            OnStateChange?.Invoke(_currentState.State);
        }
    }

    public System.Action<string> OnStateChange;

    protected void Awake()
    {
        ownerMainData = GetComponent<AMainData>();
        ownerMainData.stateManager = this;
        _currentState = getStateDataByState(InitState);// states[0].State);
        TriggerStartState(_currentState);

        /*_activeStates = new List<SState>() { states[0] };
        foreach (SState item in _activeStates)
        {
            TriggerStartState(item);
        }*/
    }

    protected virtual void Update()
    {
        _currentState.StateScript.ExecuteState();
        /*foreach (SState item in _activeStates)
        {
            item.StateScript.ExecuteState();
        }*/
    }

    protected virtual void TriggerEndState(SState triggerSState)
    {
        triggerSState.StateScript.EndState();
    }

    protected virtual void TriggerStartState(SState triggerSState)
    {
        triggerSState.StateScript.StartState(ownerMainData);
    }

    protected SState getStateDataByState(string eEnemyState)
    {
        foreach (SState sState in states)
        {
            if (sState.State == eEnemyState)
                return sState;
        }
        return states[0];
    }
}
