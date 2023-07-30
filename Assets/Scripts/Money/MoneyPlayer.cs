using System;
using UnityEngine;
using PoolInterface;
using StructHouse;
using SystemObject;

public class MoneyPlayer : MonoBehaviour, IReturnable<MoneyPlayer>, IHaveMoney, ISetActiveObject
{
    private Action<MoneyPlayer> _onReturn;

    private IReturn<MoneyPlayer> _pool;

    [SerializeField] private int _countMoney;

    public int CountMoney { get { return _countMoney; } private set { } }

    private void OnDisable()
    {
        _onReturn?.Invoke(this);
    }

    private void OnDestroy()
    {
        _onReturn -= _pool.Return;
    }

    public void GetT(IReturn<MoneyPlayer> pool)
    {
        _pool = pool;

        _onReturn += _pool.Return;
    }

    public Vector3 MyPos() => transform.position;    

    public int GiveMoney() => _countMoney;

    public void OnEnableObject(IChangeActiveObject active) => gameObject.SetActive(active.SetTrue());

    public void OnDisableObject(IChangeActiveObject active) => gameObject.SetActive(active.SetFalse());
}
