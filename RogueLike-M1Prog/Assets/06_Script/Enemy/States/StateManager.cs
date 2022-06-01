using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StateManager : MonoBehaviour
{
    MainData ownerMainData;

    private int indexState;
    public struct SState
    {
        public EEnemyState State;
        public State StateScript;
    }
    public SState[] states;

    public enum EEnemyState
    {
        Idle,
        Chase,
        Attack,
        Patrol
    }

    [SerializeField]
    private SState _currentState;

    public EEnemyState currentState
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

    public System.Action<EEnemyState> OnStateChange;

    private void Awake()
    {
        ownerMainData = GetComponent<MainData>();
        ownerMainData.stateManager = this;
        _currentState = getStateDataByState(EEnemyState.Patrol);
    }

    void Update()
    {
        _currentState.StateScript.ExecuteState();
    }

    void TriggerEndState(SState triggerSState)
    {
        triggerSState.StateScript.EndState();
    }

    void TriggerStartState(SState triggerSState)
    {
        triggerSState.StateScript.StartState(ownerMainData);
    }

    SState getStateDataByState(EEnemyState eEnemyState)
    {
        foreach (SState sState in states)
        {
            if (sState.State == eEnemyState)
                return sState;
        }
        return states[0];
    }
}
