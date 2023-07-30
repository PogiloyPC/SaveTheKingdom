using System.Collections;
using UnityEngine;
using UnitStruct;
using InterfaceTask;

public class Fishing : MonoBehaviour, ITask, IFieldForFish
{
    [SerializeField] private Fish _fish;

    [SerializeField] private float _countOfShovelsFinished;
    private float _countOfShovelsCurrent;

    public void Start()
    {
        _fish.InitFish(this);
    }

    public void CompleteTheTask(ITasker unit)
    {
        _countOfShovelsCurrent += unit.GetDamage();

        if (_countOfShovelsCurrent >= _countOfShovelsFinished)
        {
            _countOfShovelsCurrent -= _countOfShovelsCurrent;

            _fish.PullFish(this);
        }
    }

    public float FinishValue() => _countOfShovelsFinished;

    public bool IsPulled() => true;

    public Vector3 MyPos() => transform.position;
}
