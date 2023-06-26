using System.Collections.Generic;
using UnityEngine;
using StructHouse;
using CountryModifi;

public class Farm : House
{
    [SerializeField] private List<Weed> _fieldWeed;

    private List<Vector3> _fields = new List<Vector3>();

    [SerializeField] private int _countField;

    private IDeliveryTask _country;

    private void OnEnable()
    {
        Field();

        _country = GameObject.Find("Country").GetComponent<Country>();

        for (int i = 0; i < _fieldWeed.Count; i++)
            _country.DeliveryTask(_fieldWeed[i]);
    }

    private void Field()
    {
        for (int i = 0; i < _countField / 2; i++)
        {
            _fields.Add(transform.position + new Vector3(i + 0.5f, -0.5f, 0f));
            _fields.Add(transform.position + new Vector3(-i - 0.5f, -0.5f, 0f));
        }

        for (int i = 0; i < _fieldWeed.Count; i++)
            _fieldWeed[i].transform.position = _fields[i];
    }   

    private void OnDrawGizmos()
    {
        if (_countField % 2 != 0)
            _countField += 1;

        for (int i = 0; i < _countField / 2; i++)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position + new Vector3(i + 0.5f, -0.5f, 0f), new Vector2(1, 0.1f));
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(transform.position + new Vector3(-i - 0.5f, -0.5f, 0f), new Vector2(1, 0.1f));
        }
    }
}
