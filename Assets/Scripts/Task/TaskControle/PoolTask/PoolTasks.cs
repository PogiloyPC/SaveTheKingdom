using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StructHouse;
using UnitStruct;
using InterfaceTask;

public class PoolTasks<T, U> : IReturnUnit<T> where T : ICompleteTheTask<U> where U : ITask
{
    private Queue<T> _freeCitizens = new Queue<T>();
    private Queue<U> _tasks = new Queue<U>();

    private List<T> _citizens = new List<T>();

    public void GetUnit(T citizen)
    {
        _freeCitizens.Enqueue(citizen);

        Debug.Log(_freeCitizens.Count);

        CheckFreeTask();
    }

    public void GetTasks(U task)
    {
        _tasks.Enqueue(task);

        Debug.Log(_tasks.Count);

        CheckFreeTask();
    }

    private void CheckFreeTask()
    {
        int number = 0;

        if (_tasks.Count > _freeCitizens.Count)
            number = _freeCitizens.Count;
        else
            number = _tasks.Count;


        for (int i = 0; i < number; i++)
        {
            if (_tasks.Count > 0 && _freeCitizens.Count > 0)
            {
                _citizens.Add(_freeCitizens.Peek());

                _freeCitizens.Dequeue().TakeTask(_tasks.Dequeue());
            }
        }
    }

    public void Return(T unit)
    {
        _tasks.Enqueue(unit.ReturnTask());

        OnCheckUnit(unit);
    }

    public void ReturnUnit(T unit)
    {
        _freeCitizens.Enqueue(unit);

        Debug.Log(_freeCitizens.Count);

        OnCheckUnit(unit);
    }

    private void OnCheckUnit(T unit)
    {
        _citizens.Remove(unit);

        CheckFreeTask();
    }
}
