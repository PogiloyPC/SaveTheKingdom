using UnityEngine;
using UnitStruct;
using InterfaceTask;

public class Cutting : TaskCountry
{
    [SerializeField] private MoneyPlayer _money;

    [SerializeField, Range(1f, 3f)] private float _healthWood;
    public float HealthWood { get { return _healthWood; } private set { } }

    public override void CompleteTheTask(ITasker unit)
    {
        _healthWood -= unit.GetDamage();

        if (_healthWood <= 0f)
        {
            unit.FinishedTask();

            TaskComplete();
        }
    }    

    private void TaskComplete()
    {
        Instantiate(_money, transform.position, Quaternion.identity);

        Destroy(gameObject, 1f);
    }
}
