using UnityEngine;
using UnitStruct;

public class StoneMining : TaskCountry
{
    [SerializeField] private MoneyPlayer _money;

    [SerializeField, Range(1f, 3f)] private float _healthStone;
    public float HealthStone { get { return _healthStone; } private set { } }

    public override void CompleteTheTask(ITasker unit)
    {
        _healthStone -= unit.GetDamage();

        if (_healthStone <= 0f)
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
