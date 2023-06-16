using UnityEngine;

public class StateMachineUnit 
{
    private UnitState _stateUnit;

    public void InitState(UnitState stateUnit)
    {
        _stateUnit = stateUnit;
        _stateUnit.EnterState();
    }

    public void ChangeState(UnitState stateUnit)
    {
        if (_stateUnit != stateUnit)
        {
            _stateUnit.ExitState();
            _stateUnit = stateUnit;
            _stateUnit.EnterState();
        }
    }

    public void Update()
    {
        _stateUnit.PlayState();
    }
}
