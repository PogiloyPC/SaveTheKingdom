using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CellBuyItem : MonoBehaviour, IPointerDownHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Image _timerBuy;

    [SerializeField] private Item _item;

    [SerializeField] private Forge _forge;

    [SerializeField] private int _priceItem;

    private Player _player;

    private float _currentTime;
    private float _finishTime = 1.5f;

    private bool _isBuy;

    private int _maxBuyItem = 8;

    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_forge.Items.Count < _maxBuyItem && _player.Wallet.MoneyCount >= _priceItem)
            _isBuy = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_isBuy)
        {
            _currentTime += Time.deltaTime;

            _timerBuy.fillAmount = _currentTime / _finishTime;

            if (_currentTime >= _finishTime)
                CreateItem();
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (_currentTime >= _finishTime)
            CreateItem();
        else
            OnEndBuy();
    }

    private void CreateItem()
    {
        _player.Wallet.Pay(_priceItem);

        _forge.CreateItem(_item);

        OnEndBuy();
    }

    private void OnEndBuy()
    {
        _isBuy = false;

        _currentTime = 0f;

        _timerBuy.fillAmount = 0f;
    }
}
