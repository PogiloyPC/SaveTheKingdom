using System.Collections;
using UnityEngine;
using UnitStruct;
using InterfaceTask;

public class Fishing : MonoBehaviour, ITask
{
    [SerializeField] private MoneyPlayer _money;

    [SerializeField] private float _countOfShovelsFinished;
    private float _countOfShovelsCurrent;    

    public void CompleteTheTask(ITasker unit)
    {
        _countOfShovelsCurrent += unit.GetDamage();

        if (_countOfShovelsCurrent >= _countOfShovelsFinished)
        {
            _countOfShovelsCurrent -= _countOfShovelsCurrent;

            Harvest();
        }
    }

    public Vector3 MyPos()
    {
        return transform.position;
    }

    private void Harvest()
    {
        Rigidbody2D rb = Instantiate(_money, transform.position, Quaternion.identity).GetComponent<Rigidbody2D>();
        rb.AddForce(transform.up * 4f, ForceMode2D.Impulse);
    }
}
