using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObjects<T> where T : MonoBehaviour
{
    private List<T> _projects;   

    private Transform _transformList;

    private T _prefab;

    private bool _isAutomatic;    

    public PoolObjects(List<T> projects, Transform pos, bool isAutomatic, T prefab)
    {
        _projects = projects;

        _transformList = pos;

        _isAutomatic = isAutomatic;

        _prefab = prefab;            
    }

    public T PullOutPool()
    {
        bool noOne = false;

        for (int i = 0; i < _projects.Count; i++)
        {
            if (_projects[i] == null)
            {
                _projects.Remove(_projects[i]);
            }
            else if (_projects[i].gameObject.activeSelf == false)
            {
                _projects[i].gameObject.SetActive(true);
                
                return _projects[i];
            }
            else
            {
                noOne = true;
            }
        }

        if (noOne && _isAutomatic)
            return _prefab = CreatePrefab();

        return null;
    }

    private T CreatePrefab()
    {
        T project = GameObject.Instantiate(_prefab);

        _projects.Add(project);

        project.transform.SetParent(_transformList, true);

        return project;
    }
}
