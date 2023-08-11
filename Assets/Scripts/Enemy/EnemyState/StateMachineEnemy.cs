using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachineEnemy
{
    private EnemyState _state;

    public void InitState(EnemyState state)
    {
        _state = state;
        _state.EnterState();
    }

    public void ChangeState(EnemyState state)
    {
        if (_state != state)
        {
            _state.ExitState();
            _state = state;
            _state.EnterState();
        }
    }

    

    public void UpdateState()
    {
        _state.PlayState();
    }

}
