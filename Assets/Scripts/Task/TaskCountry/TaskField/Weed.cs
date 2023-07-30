using System.Collections;
using UnityEngine;
using UnitStruct;
using InterfaceTask;

public class Weed : MonoBehaviour, ITask, IFieldForMillet
{
    [SerializeField] private Millet _millet;

    [SerializeField] private float _countOfShovelsFinish = 10f;
    private float _countOfShovelsCurrent;

    public void CompleteTheTask(ITasker unit)
    {
        _countOfShovelsCurrent += unit.GetDamage();

        _millet.WeedMillet(this);

        if (_countOfShovelsCurrent >= _countOfShovelsFinish)        
            _countOfShovelsCurrent -= _countOfShovelsCurrent;                  
    }

    public float CurrentValue() => _countOfShovelsCurrent;   
    
    public float FinishValue() => _countOfShovelsFinish;    

    public Vector3 MyPos() => transform.position;    
}
