using System.Collections.Generic;
using UnityEngine;
using PlayerModification;
using StructHouse;

public class Forge : House, IChangeActive
{
    [SerializeField] private Transform _posSpawn;
    
    [SerializeField] private CellBuyItem[] _cellsBuyObjects;
    
    [SerializeField] private Stack<Item> _items = new Stack<Item>();
    public IReadOnlyCollection<Item> Items => _items;

    private int _maxCellsShop = 4;

    private bool _thereIsABuyer;

    [SerializeField] private float _offsetX;

    private void Start()
    {       
    }

    public void CreateItem(Item item)
    {
        Vector3 direction;

        Item itemObj = Instantiate(item);

        if (_items.Count < 1)
            direction = _posSpawn.position;
        else
            direction = new Vector3(_items.Peek().transform.position.x + _offsetX,
                _items.Peek().transform.position.y);

        itemObj.transform.position = direction;

        _items.Push(itemObj);
    }

    private void ActivateShopForge()
    {        
        for (int i = 0; i < _maxCellsShop + Mathf.Clamp(LevelHouse, 0, _maxCellsShop + 1); i++)
            _cellsBuyObjects[i].SetActive(this);
    }

    public Item DeleteItem()
    {
        Item item = _items.Pop();        

        return item;
    }

    public bool OnSwitchObject()
    {
        return _thereIsABuyer;
    }    

    protected override void OnEnterObject(Collider2D other)
    {
        IBuyer player = other.gameObject.GetComponent<Player>();

        if (player != null)
        {
            _thereIsABuyer = true;

            ActivateShopForge();
        }
    }

    protected override void OnExitObject(Collider2D other)
    {
        IBuyer player = other.gameObject.GetComponent<Player>();

        if (player != null)
        {
            _thereIsABuyer = false;

            ActivateShopForge();
        }
    }
}
