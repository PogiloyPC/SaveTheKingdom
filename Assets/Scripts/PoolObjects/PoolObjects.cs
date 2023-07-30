using System.Collections.Generic;
using SystemObject;
using UnityEngine;
using StructHouse;
using PoolInterface;

public class PoolObjects<T> : IReturn<T>, IChangeActiveObject where T : MonoBehaviour, IReturnable<T>, ISetActiveObject
{
    private Queue<T> _projectails = new Queue<T>();

    private Transform _transformList;

    private T _prefab;

    private bool _isAutomatic;

    public PoolObjects(Transform pos, bool isAutomatic, T prefab, bool createInStart, int countObjects)
    {
        _transformList = pos;

        _isAutomatic = isAutomatic;

        _prefab = prefab;

        if (createInStart)
            for (int i = 0; i < countObjects; i++)
                CreateObjects();        
    }

    private void CreateObjects()
    {
        T project = GameObject.Instantiate(_prefab);

        project.OnDisableObject(this);
        project.transform.SetParent(_transformList, true);
        project.GetT(this);

        _projectails.Enqueue(project);
    }

    public T PullOutPool()
    {
        bool noOne;

        noOne = _projectails.Count == 0;

        if (!noOne)
        {                        
            _projectails.Peek().OnEnableObject(this);

            return _projectails.Dequeue();
        }
        else if (noOne && _isAutomatic)
        {
            return CreatePrefab();
        }

        return null;
    }

    public void Return(T t) => _projectails.Enqueue(t);

    private T CreatePrefab()
    {
        T project = GameObject.Instantiate(_prefab);

        project.GetT(this);
        project.transform.SetParent(_transformList, true);

        return project;
    }

    public int CountObject() => _projectails.Count;

    public bool SetTrue() => true;
    
    public bool SetFalse() => false;            
}
