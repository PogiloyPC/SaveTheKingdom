using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StructHouse;

public class Lake : House, IHaveField
{
    [SerializeField] private Transform[] _pos;

    [SerializeField, Range(0, 1)] private float _offset;

    private List<Vector3> _field = new List<Vector3>();

    [SerializeField] private int _countField;

    private IGetHouseFields _country;

    private void OnEnable()
    {
        Field();

        _country = GameObject.Find("Country").GetComponent<Country>();

        _country.GetFields(this);
    }

    private void Field()
    {
        for (int i = 0; i < _countField / 2; i++)
        {
            _field.Add(transform.position + new Vector3(i - 0.75f, -0.5f, 0f));
        }

        for (int i = 0; i < _countField / 2; i++)
        {
            _field.Add(transform.position + new Vector3(-i + 0.75f, -0.5f, 0f));
        }       
    }

    public List<Vector3> Fields()
    {
        return _field;
    }

    public int CountFields()
    {
        return _countField;
    }

    private void OnDrawGizmos()
    {
        if (_countField % 2 != 0)
            _countField += 1;

        for (int i = 0; i < _countField / 2; i++)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position + new Vector3(i - 0.75f, -0.5f, 0f), new Vector2(0.25f, 0.1f));
        }

        for (int i = 0; i < _countField / 2; i++)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(transform.position + new Vector3(-i + 0.75f, -0.5f, 0f), new Vector2(0.25f, 0.1f));
        }
    }
}
