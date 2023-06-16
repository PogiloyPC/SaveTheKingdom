using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ControleHouse : MonoBehaviour
{        
    [SerializeField] private House _houseObj;

    [SerializeField] private float _timerCreater;
    private float _currentTimerCreater;
    [SerializeField] private float _finishTimeForUpgrade;
    private float _currentTimeForUpgrade;

    [SerializeField] private int _priceBuildHouse;
    [SerializeField] private int _priceForUpgrade;

    [SerializeField] private UnityEvent<float, float, bool> _onChangeTimeForUpgrade;
    [SerializeField] private UnityEvent<string, string, string, bool> _onDisplayInfoHouse;

    private bool _isImprovable;
    private bool _isUpgraded;

    private Player _player;

    private SizeCountry _sizeCountry;

    private void Start()
    {
        _sizeCountry = GameObject.Find("SizeCountry").GetComponent<SizeCountry>();
    }

    private void OnMouseDown()
    {
        if (_player.Wallet.MoneyCount >= _priceForUpgrade)
            _isUpgraded = true;
    }

    private void OnMouseDrag()
    {
        if (_isImprovable && _isUpgraded)
            CheckCurrentTime();
    }

    private void OnMouseUp()
    {
        if (_currentTimeForUpgrade >= _finishTimeForUpgrade)
            CheckHouse();
        else
            ReloadAction();
    }

    private void CheckCurrentTime()
    {
        _currentTimeForUpgrade += Time.deltaTime;

        _onChangeTimeForUpgrade?.Invoke(_currentTimeForUpgrade, _finishTimeForUpgrade, true);

        if (_currentTimeForUpgrade >= _finishTimeForUpgrade)
            CheckHouse();

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

        _player.Wallet.Pay(_priceForUpgrade);

        _priceForUpgrade += _priceForUpgrade / 3;

        ReloadAction();
    }

    private void ReloadAction()
    {
        _currentTimeForUpgrade = 0f;

        _isUpgraded = false;

        _onChangeTimeForUpgrade?.Invoke(_currentTimeForUpgrade, _finishTimeForUpgrade, false);
    }

    private void BuildHouse()
    {
        _player.Wallet.Pay(_priceBuildHouse);

        ReloadAction();

        StartCoroutine(CreateHouse());
    }

    private IEnumerator CreateHouse()
    {
        _currentTimerCreater = _timerCreater;

        while (true)
        {
            _currentTimerCreater -= Time.deltaTime;

            if (_currentTimerCreater <= 0f)
            {
                _houseObj.gameObject.SetActive(true);

                _sizeCountry.AddHouse(_houseObj);

                break;
            }

            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.gameObject.GetComponent<Player>();

        if (player != null)
        {
            _isImprovable = true;
          
            _player = player;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        Player player = other.gameObject.GetComponent<Player>();

        if (player != null)        
            if (_houseObj.gameObject.activeSelf)
                _onDisplayInfoHouse?.Invoke(_houseObj.NameHouse, _houseObj.LevelHouse.ToString(), _houseObj.HealthHouse.ToString(), true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Player player = other.gameObject.GetComponent<Player>();

        if (player != null)
        {
            _isImprovable = false;

            if (_houseObj.gameObject.activeSelf)
                _onDisplayInfoHouse?.Invoke("", "", "", false);

            _player = null;
        }
    }
}
