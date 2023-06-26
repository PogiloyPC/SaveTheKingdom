using UnityEngine;
using System;
using UnitStruct;
using StructHouse;
using InterfaceTask;

public class Lumberman : UnitCitizen, ICompleteTheTask<Cutting>, ITasker
{
    private ITask _cutting;

    private Action<Lumberman> _onDead;
    private Action<Lumberman> _onCompleteTask;

    private IReturnUnit<Lumberman> _taskControle;

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
                task.LookTaskPos(_cutting.MyPos());            

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

        _cutting = null;

        _onCompleteTask?.Invoke(this);

        GetStateMachineUnit().ChangeState(GetMoveState());
    }

    public void DoingTask()
    {
        _cutting.CompleteTheTask(this);
    }

    public void GetTaskControle(IReturnUnit<Lumberman> taskControle)
    {
        _taskControle = taskControle;

        _onCompleteTask += _taskControle.ReturnUnit;
        _onDead += _taskControle.Return;
    }

    public Cutting ReturnTask()
    {
        return (Cutting)_cutting;
    }

    public void TakeTask(Cutting task)
    {
        _cutting = task;

        _isWorked = true;
    }

    public float GetDamage()
    {
        return _damage;
    }
}
