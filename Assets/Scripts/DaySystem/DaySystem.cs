using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DaySystem : MonoBehaviour
{
    [SerializeField] private UnityEvent _onFinishedDay;
    [SerializeField] private UnityEvent _onStartNight;

    [SerializeField] private AnimationCurve _dayCurve;

    [SerializeField] private ParticleSystem _stars;

    [SerializeField] private List<ChangeFire> _fire;

    [SerializeField] private Light _dayLight;
    [SerializeField] private Light _playerLight;
    [SerializeField] private Light[] _allLightCountry;

    [SerializeField] private Material _dayBox;
    [SerializeField] private Material _nightBox;

    private Camera _cam;

    [SerializeField] private float _dayLength;
    private float _dayTime = 1f;
    public float DayLength { get { return _dayLength; } private set { } }
    public float CurrentDayTime { get; private set; }
    public float DayTime { get { return _dayTime; } private set { } }


    private void Start()
    {
        _cam = Camera.main;

        for (int i = 0; i < _fire.Count; i++)
        {
            _onFinishedDay.AddListener(_fire[i].OnDisableBurn);
            _onStartNight.AddListener(_fire[i].OnBurn);
        }
    }

    private void OnEnable()
    {
        for (int i = 0; i < _fire.Count; i++)
        {
            _onFinishedDay.AddListener(_fire[i].OnDisableBurn);
            _onStartNight.AddListener(_fire[i].OnBurn);
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < _fire.Count; i++)
        {
            _onFinishedDay.RemoveListener(_fire[i].OnDisableBurn);
            _onStartNight.RemoveListener(_fire[i].OnBurn);
        }
    }

    private void Update()
    {
        CurrentDayTime += Time.deltaTime / _dayLength;

        //Debug.Log(CurrentDayTime);

        var cam = _cam.backgroundColor;
        cam = Color.Lerp(_dayBox.color, _nightBox.color, 1 - _dayCurve.Evaluate(CurrentDayTime));
        _cam.backgroundColor = cam;

        _dayLight.intensity = _dayCurve.Evaluate(CurrentDayTime);
        _playerLight.intensity = 1 - _dayCurve.Evaluate(CurrentDayTime);

        for (int i = 0; i < _allLightCountry.Length; i++)
            _allLightCountry[i].intensity = 1 - _dayCurve.Evaluate(CurrentDayTime);

        _stars.startColor = new Color(1f, 1f, 1f, 1 - _dayCurve.Evaluate(CurrentDayTime));

        if (CurrentDayTime >= 0.6f && CurrentDayTime <= 0.6f + Time.deltaTime / _dayLength)
        {
            _onStartNight.Invoke();
        }
        else if (CurrentDayTime >= _dayTime)
        {
            _onFinishedDay?.Invoke();

            CurrentDayTime -= _dayTime;
        }
    }
}
