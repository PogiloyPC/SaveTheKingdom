using System.Collections.Generic;
using UnityEngine;
using StructHouse;
using UnitStruct;

public class PoolFields<T, U> : IReturn<T> where T : IWorkInField where U : IHaveField
{
    private Queue<T> _freeCitizens = new Queue<T>();
    private Queue<Vector3> _fields = new Queue<Vector3>();

    private List<T> _citizens = new List<T>();

    public void GetUnit(T citizen)
    {
        _freeCitizens.Enqueue(citizen);        

        CheckFreeField();
    }

    public void GetFields(U placeWork)
    {
        for (int i = 0; i < placeWork.CountFields(); i++)
            _fields.Enqueue(placeWork.Fields()[i]);        

        CheckFreeField();
    }

    private void CheckFreeField()
    {
        int number = 0;

        if (_fields.Count > _freeCitizens.Count)
            number = _freeCitizens.Count;
        else
            number = _fields.Count;


        for (int i = 0; i < number; i++)
        {
            if (_fields.Count > 0 && _freeCitizens.Count > 0)
            {
                _citizens.Add(_freeCitizens.Peek());                

                _freeCitizens.Dequeue().TakePosField(_fields.Dequeue());               
            }
        }        
    }

    public void Return(T unit)
    {        
        _fields.Enqueue(unit.PosWork());

        _citizens.Remove(unit);        

        CheckFreeField();
    }
}
