using UnityEngine;
using System;
using StructHouse;
using UnitStruct;

public class Farmer : UnitCitizen, IWorkInField
{
    private IReturn<Farmer> _farmSystem;

    private Action<Farmer> _onDead;

    private Vector3 _posWork;

    [SerializeField] private bool _haveField;
    
    protected override void StartCitizenUnit()
    {
        InitState(new FieldWorkState(this, GetComponent<Animator>(), transform, Speed), new MoveState(this, GetComponent<Animator>()));
    }

    private void Update()
    {
        if (Countr.CurrentDayTime <= 0.6f && _haveField)
            GetStateMachineUnit().ChangeState(GetTaskState());
        else
            GetStateMachineUnit().ChangeState(GetMoveState());

        GetStateMachineUnit().Update();
    }

    public void TakePosField(Vector3 posWork)
    {
        _posWork = posWork;

        _haveField = true;
    }

    private void OnDestroy()
    {
        _onDead.Invoke(this);

        _onDead -= _farmSystem.Return;
    }

    public void GetFarmSystem(IReturn<Farmer> farmSystem)
    {
        _farmSystem = farmSystem;

        _onDead += _farmSystem.Return;
    }

    public Vector3 PosWork() => _posWork;
    
}
