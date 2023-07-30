using UnityEngine;
using System;
using SystemObject;
using InterfaceTask;
using PoolInterface;
using StructHouse;

public class Mark : MonoBehaviour, IReturnable<Mark>, ISetActiveObject
{
    private Action<Mark> _onReturn;

    private IReturn<Mark> _pool;

    private void OnDisable()
    {
        _onReturn?.Invoke(this);
    }

    public void GetT(IReturn<Mark> pool)
    {
        _pool = pool;

        _onReturn += _pool.Return;
    }

    public void AttachMark(ITaskLabel task) => transform.position = task.MyPos();

    public void OnEnableObject(IChangeActiveObject active) => gameObject.SetActive(active.SetTrue());

    public void OnDisableObject(IChangeActiveObject active) => gameObject.SetActive(active.SetFalse());
}