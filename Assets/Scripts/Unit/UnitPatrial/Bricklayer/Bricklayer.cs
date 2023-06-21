using UnityEngine;
using System;
using UnitStruct;
using StructHouse;

public class Bricklayer : UnitCitizen, ICompleteTheTask<StoneMining>, ITasker
{
    private StoneMining _stoneMining = null;

    private Action<Bricklayer> _onDead;
    private Action<Bricklayer> _onCompleteTask;

    private IReturnUnit<Bricklayer> _taskControle;

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
                task.LookTaskPos(_stoneMining.transform.position);
            
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

        _stoneMining = null;

        _onCompleteTask.Invoke(this);

        GetStateMachineUnit().ChangeState(GetMoveState());
    }

    public void DoingTask()
    {
        _stoneMining.CompleteTheTask(this);       
    }

    public void GetTaskControle(IReturnUnit<Bricklayer> taskControle)
    {
        _taskControle = taskControle;

        _onCompleteTask += _taskControle.ReturnUnit;
        _onDead += _taskControle.Return;
    }

    public StoneMining GetTask()
    {
        return _stoneMining;
    }

    public void TakeTask(StoneMining task)
    {
        _stoneMining = task;

        _isWorked = true;
    }

    public float GetDamage()
    {
        return _damage;
    }
}
