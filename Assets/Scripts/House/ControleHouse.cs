using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using PlayerModification;
using CountryModifi;
using UnitStruct;

public class ControleHouse : TaskCountry
{
    [SerializeField] private House _houseObj;

    [SerializeField] private float _finishTimeForUpgrade;
    public float CurrentTimeForUpgrade { get; private set; }
    public float FinishTime { get { return _finishTimeForUpgrade; } private set { } }

    [SerializeField] private int _priceBuildHouse;
    [SerializeField] private int _priceForUpgrade;

    [SerializeField] private UnityEvent<float, float, bool> _onChangeTimeForUpgrade;
    [SerializeField] private UnityEvent<string, string, string, bool> _onDisplayInfoHouse;

    private bool _isImprovable;
    private bool _isUpgraded;

    private IBuyer _player;

    private IDistributeTasks _distributeTasks;

    private SizeCountry _sizeCountry;

    private void Start()
    {
        _sizeCountry = GameObject.Find("SizeCountry").GetComponent<SizeCountry>();

        _distributeTasks = GameObject.Find("Country").GetComponent<Country>();
    }

    private void OnMouseDown()
    {
        if (_player.MoneyCount() >= _priceForUpgrade)
            _isUpgraded = true;
    }

    private void OnMouseDrag()
    {
        if (_isImprovable && _isUpgraded)
            CheckCurrentTime();
    }

    private void OnMouseUp()
    {
        if (CurrentTimeForUpgrade >= _finishTimeForUpgrade)
        {
            _distributeTasks.DistributeTasks(this);

            PayForTheBuild();

            ReloadAction();
        }
        else
        {
            ReloadAction();
        }
    }

    public override void CompleteTheTask(ITasker unit)
    {
        CurrentTimeForUpgrade += unit.GetDamage();

        if (CurrentTimeForUpgrade >= _finishTimeForUpgrade)
        {
            unit.FinishedTask();

            CheckHouse();
        }
    }

    private void CheckCurrentTime()
    {
        CurrentTimeForUpgrade += Time.deltaTime;

        _onChangeTimeForUpgrade?.Invoke(CurrentTimeForUpgrade, _finishTimeForUpgrade, true);

        if (CurrentTimeForUpgrade >= _finishTimeForUpgrade)
        {
            _distributeTasks.DistributeTasks(this);

            PayForTheBuild();

            ReloadAction();
        }
    }

    private void CheckHouse()
    {
        if (_houseObj.gameObject.activeSelf)
            UpgradeHouse();
        else
            BuildHouse();
    }

    private void UpgradeHouse()
    {
        _houseObj.LevelUp();       

        ReloadAction();
    }

    private void PayForTheBuild()
    {
        _player.WantPay().Pay(_priceForUpgrade);

        _priceForUpgrade += _priceForUpgrade / 3;
    }

    private void ReloadAction()
    {
        CurrentTimeForUpgrade = 0f;

        _isUpgraded = false;

        _onChangeTimeForUpgrade?.Invoke(CurrentTimeForUpgrade, _finishTimeForUpgrade, false);
    }

    private void BuildHouse()
    {       
        _houseObj.gameObject.SetActive(true);

        _sizeCountry.AddHouse(_houseObj);

        ReloadAction();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        IBuyer player = other.gameObject.GetComponent<Player>();

        if (player != null)
        {
            _isImprovable = true;

            _player = player;

            if (_houseObj.gameObject.activeSelf)
                _onDisplayInfoHouse?.Invoke(_houseObj.NameHouse, _houseObj.LevelHouse.ToString(), _houseObj.HealthHouse.ToString(), true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        IBuyer player = other.gameObject.GetComponent<Player>();

        if (player != null)
        {
            _isImprovable = false;

            if (_houseObj.gameObject.activeSelf)
                _onDisplayInfoHouse?.Invoke("", "", "", false);
        }
    }
}
