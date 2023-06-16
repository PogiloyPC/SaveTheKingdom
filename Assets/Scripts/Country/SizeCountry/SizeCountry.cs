using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeCountry : MonoBehaviour
{
    [SerializeField] private List<House> _houses;

    [SerializeField, Range(0, 2)] private float _minDistanceBorders;

    public Vector3 RightBorders { get; private set; }
    public Vector3 LeftBorders { get; private set; } 

    private void Start()
    {
        RightBorders = new Vector3(_minDistanceBorders, 0f, 0f);
        LeftBorders = new Vector3(-_minDistanceBorders, 0f, 0f);
    }

    public void AddHouse(House house)
    {
        _houses.Add(house);

        FindRightBorders();
        FindLeftBorders();        
    }

    private void FindRightBorders()
    {
        for(int i = 0; i < _houses.Count; i++)        
            if (_houses[i].transform.position.x > RightBorders.x)
                RightBorders = _houses[i].transform.position;      
    }

    private void FindLeftBorders()
    {
        for (int i = 0; i < _houses.Count; i++)
            if (_houses[i].transform.position.x < LeftBorders.x)
                LeftBorders = _houses[i].transform.position;           
    }          
}
