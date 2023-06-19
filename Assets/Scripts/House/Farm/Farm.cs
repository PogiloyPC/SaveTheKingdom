using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farm : House
{
    [SerializeField] private Transform[] _pos;

    private List<Vector3> _field = new List<Vector3>();

    public List<Vector3> FieldPos => _field;

    [SerializeField] private int _countField;

    public int CountField { get { return _countField; } private set { } }

    private Country _country;
    
    private void OnEnable()
    {
        Field();

        _country = GameObject.Find("Country").GetComponent<Country>();

        _country.GetFarm(this);
    }    

    private void Field()
    {
        for (int i = 0; i < _countField / 2; i++)
        {            
            _field.Add(transform.position + new Vector3(i + 0.5f, -0.5f, 0f));
        }

        for (int i = 0; i < _countField / 2; i++)
        {
            _field.Add(transform.position + new Vector3(-i - 0.5f, -0.5f, 0f));
        }

        for (int i = 0; i < _pos.Length; i++)
        {
            _pos[i].position = _field[i];
        }
    }

    private void OnDrawGizmos()
    {
        if (_countField % 2 != 0)
            _countField += 1;

        for (int i = 0; i < _countField / 2; i++)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position + new Vector3(i + 0.5f, -0.5f, 0f), new Vector2(1, 0.1f));
        }

        for (int i = 0; i < _countField / 2; i++)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(transform.position + new Vector3(-i - 0.5f, -0.5f, 0f), new Vector2(1, 0.1f));
        }
    }
}
