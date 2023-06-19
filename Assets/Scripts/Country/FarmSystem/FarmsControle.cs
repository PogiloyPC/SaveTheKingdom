using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmsControle
{
    private Queue<Farmer> _freeFarmers = new Queue<Farmer>();
    private Queue<Vector3> _fields = new Queue<Vector3>();

    private List<Farmer> _farmers = new List<Farmer>();

    public void GetFarmer(Farmer farmer)
    {
        _freeFarmers.Enqueue(farmer);

        Debug.Log(_freeFarmers.Count);

        CheckFreeField();
    }

    public void GetFields(Farm farm)
    {
        for (int i = 0; i < farm.CountField; i++)
            _fields.Enqueue(farm.FieldPos[i]);

        Debug.Log(_fields.Count);

        CheckFreeField();
    }

    private void CheckFreeField()
    {
        int number = 0;

        if (_fields.Count > _freeFarmers.Count)
            number = _freeFarmers.Count;
        else
            number = _fields.Count;


        for (int i = 0; i < number; i++)
        {
            if (_fields.Count > 0 && _freeFarmers.Count > 0)
            {
                _farmers.Add(_freeFarmers.Peek());

                _freeFarmers.Dequeue().TakePosField(_fields.Dequeue());

                Debug.Log(_fields.Count);
            }            
        }
    }
}
