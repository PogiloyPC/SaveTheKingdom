using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using PlayerModification;
using CountryModifi;
using UnitStruct;
using InterfaceTask;

public class ControleHouse : MonoBehaviour, ITask
{
    [SerializeField] private House _houseObj;

    [SerializeField] private UnityEvent<float, float, bool> _onChangeTimeForUpgrade;
    [SerializeField] private UnityEvent<string, string, string, bool> _onDisplayInfoHouse;

    private SizeCountry _sizeCountry;

    private IBuyer _player;

    private IDeliveryTask _devileryTasks;

    [SerializeField] private float _buildingTimeFinish;
    private float _buildingTimeCurrent;
    [SerializeField] private float _finishTimeBuy;
    private float _currentTimeBuy;
    [SerializeField] private float _radiusCircle;
    [SerializeField] private LayerMask _maskLayer;

    [SerializeField] private int _priceForBuild;

    private bool _isImprovable;
    private bool _isUpgraded;
    private bool _isBuilding;
    private bool _spaceClear;

    private void Start()
    {
        _sizeCountry = GameObject.Find("SizeCountry").GetComponent<SizeCountry>();

        _devileryTasks = GameObject.Find("Country").GetComponent<Country>();
    }

    private void OnMouseDown()
    {
        if (_player.MoneyCount() >= _priceForBuild && !_isBuilding)
            _isUpgraded = true;
    }

    private void OnMouseDrag()
    {
        if (_isImprovable && _isUpgraded && _spaceClear)
            CheckCurrentTime();
    }

    private void OnMouseUp()
    {
        if (_currentTimeBuy >= _finishTimeBuy)
            MarkTask();
        else
            ReloadAction();
    }

    public void CompleteTheTask(ITasker unit)
    {
        _buildingTimeCurrent += unit.GetDamage();

        if (_buildingTimeCurrent >= _buildingTimeFinish)
        {
            unit.FinishedTask();

            _isBuilding = false;

            _buildingTimeCurrent = 0f;

            CheckHouse();
        }
    }

    private void CheckCurrentTime()
    {
        _currentTimeBuy += Time.deltaTime;

        _onChangeTimeForUpgrade?.Invoke(_currentTimeBuy, _finishTimeBuy, true);

        if (_currentTimeBuy >= _finishTimeBuy)
            MarkTask();
    }

    private void MarkTask()
    {
        _devileryTasks.DeliveryTask(this);

        _isBuilding = true;

        PayForTheBuild();

        ReloadAction();
    }

    private void CheckHouse()
    {
        if (_houseObj.gameObject.activeSelf)
            UpgradeHouse();
        else
            BuildHouse();

        _buildingTimeFinish += 0.5f;
    }

    private void UpgradeHouse()
    {
        _houseObj.LevelUp();

        ReloadAction();
    }

    private void BuildHouse()
    {
        _houseObj.gameObject.SetActive(true);

        _sizeCountry.AddHouse(_houseObj);

        ReloadAction();
    }

    private void ReloadAction()
    {
        _currentTimeBuy = 0f;

        _isUpgraded = false;

        _onChangeTimeForUpgrade?.Invoke(_currentTimeBuy, _finishTimeBuy, false);
    }

    private void PayForTheBuild()
    {
        _player.WantPay().Pay(_priceForBuild);

        _priceForBuild += _priceForBuild / 3;
    }

    public Vector3 MyPos() => transform.position;

    private void CheckSpace()
    {
        Collider2D collide = Physics2D.OverlapCircle(_houseObj.transform.position, _radiusCircle, _maskLayer);

        ISurroinding task = collide?.gameObject.GetComponent<TaskCountry>();

        if (task != null)
            _spaceClear = false;
        else
            _spaceClear = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        IBuyer player = other.gameObject.GetComponent<Player>();

        if (player != null)
        {
            _isImprovable = true;

            _player = player;

            CheckSpace();

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

            CheckSpace();

            if (_houseObj.gameObject.activeSelf)
                _onDisplayInfoHouse?.Invoke("", "", "", false);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(_houseObj.transform.position, _radiusCircle);
    }
}