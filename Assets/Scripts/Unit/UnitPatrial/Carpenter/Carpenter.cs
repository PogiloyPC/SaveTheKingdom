using UnityEngine;
using System;
using UnitStruct;
using StructHouse;
using InterfaceTask;

public class Carpenter : UnitCitizen, ICompleteTheTask<ControleHouse>, ITasker
{
    private ITask _building = null;

    private Action<Carpenter> _onDead;
    private Action<Carpenter> _onCompleteTask;

    private IReturnUnit<Carpenter> _taskControle;

    [SerializeField, Range(0f, 1f)] private float _damage;

    [SerializeField] private bool _isWorked;

    protected override void StartCitizenUnit()
    {
        InitState(new CompleteTaskCountry(GetComponent<Animator>(), transform, Speed), new MoveState(this, GetComponent<Animator>()));
    }

    private void Update()
    {
        if (_isWorked && Countr.CurrentDayTime <= 0.6f)
        {
            if (GetTaskState() is CompleteTaskCountry task)
                task.LookTaskPos(_building.MyPos());

            GetStateMachineUnit().ChangeState(GetTaskState());
        }
        else
        {
            GetStateMachineUnit().ChangeState(GetMoveState());
        }

        GetStateMachineUnit().Update();
    }

    private void OnDestroy()
    {
        _onDead?.Invoke(this);

        _onCompleteTask -= _taskControle.ReturnUnit;
        _onDead -= _taskControle.Return;
    }

    public void FinishedTask()
    {
        _isWorked = false;

        _building = null;
        
        _onCompleteTask.Invoke(this);

        GetStateMachineUnit().ChangeState(GetMoveState());
    }

    public void DoingTask()
    {       
        _building.CompleteTheTask(this);
    }

    public void GetTaskControle(IReturnUnit<Carpenter> taskControle)
    {
        _taskControle = taskControle;

        _onCompleteTask += _taskControle.ReturnUnit;
        _onDead += _taskControle.Return;
    }

    public ControleHouse ReturnTask()
    {
        return (ControleHouse)_building;
    }

    public void TakeTask(ControleHouse task)
    {
        _building = task;

        _isWorked = true;
    }

    public float GetDamage()
    {
        return _damage;
    }
}
