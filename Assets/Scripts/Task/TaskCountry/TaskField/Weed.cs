using System.Collections;
using UnityEngine;
using UnitStruct;
using InterfaceTask;

public class Weed : MonoBehaviour, ITask
{
    [SerializeField] private Transform _flower;

    [SerializeField] private MoneyPlayer _money;

    [SerializeField] private float _countOfShovelsFinished;
    private float _countOfShovelsCurrent;

    [SerializeField] private int _countMoney;

    public void CompleteTheTask(ITasker unit)
    {
        _countOfShovelsCurrent += unit.GetDamage();

        _flower.position = new Vector3(transform.position.x, transform.position.y + (_countOfShovelsCurrent / _countOfShovelsFinished - 0.5f), -3);

        if (_countOfShovelsCurrent >= _countOfShovelsFinished)
        {
            _countOfShovelsCurrent -= _countOfShovelsCurrent;

            StartCoroutine(Harvest());
        }
    }

    public Vector3 MyPos()
    {
        return transform.position;
    }

    private IEnumerator Harvest()
    {
        int currentMoney = 0;
        float posX;


        while(currentMoney < _countMoney)
        {
            posX = Random.Range(-0.2f, 0.2f);

            Rigidbody2D rb = Instantiate(_money, transform.position, Quaternion.identity).GetComponent<Rigidbody2D>();
            rb.AddForce(new Vector2(posX, transform.up.y) * 4f, ForceMode2D.Impulse);

            currentMoney++;

            yield return new WaitForSeconds(0.1f);
        }
    }
}
