using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerUnitWander : MonoBehaviour
{
    [SerializeField] private DaySystem _daySystem;

    [SerializeField] private UnitWander _unitWander;

    [SerializeField] private List<UnitWander> _units;

    [SerializeField] private float _timeSpawnUnitWander;
    private float _minDistanceSpawn = 0.2f;

    private int _maxUnits = 6;

    private void Start()
    {

    }

    private void CheckUnits()
    {
        for (int i = 0; i < _units.Count; i++)
            if (_units[i] == null)
                _units.RemoveAt(i);
    }

    public void Spawn()
    {
        if (_maxUnits > _units.Count)
        {
            UnitWander unitWander = Instantiate(_unitWander, new Vector3(transform.position.x + Random.Range(-_minDistanceSpawn, _minDistanceSpawn), transform.position.y), Quaternion.identity);

            _units.Add(unitWander);
        }

        CheckUnits();
    }
}
