using System.Collections.Generic;
using UnityEngine;

public class Forge : House
{
    [SerializeField] private GameObject _shopForge;

    [SerializeField] private List<Item> _itemsShop;

    [SerializeField] private List<Item> _items;

    [SerializeField] private GameObject[] _cellsBuyObjects;

    private int _maxCellsShop = 4;

    public IReadOnlyList<Item> Items => _items;

    [SerializeField] private Transform _posSpawn;

    [SerializeField] private float _offsetX;

    private void Start()
    {
        _shopForge.SetActive(false);
    }

    public void CreateItem(Item item)
    {
        for (int i = 0; i < _itemsShop.Count; i++)
            if (_itemsShop[i].Id == item.Id)
            {
                Vector3 direction;

                Item itemObj = Instantiate(_itemsShop[i]);

                if (_items.Count < 1)
                    direction = _posSpawn.position;
                else
                    direction = new Vector3(_items[_items.Count - 1].transform.position.x + _offsetX,
                        _items[_items.Count - 1].transform.position.y);


                itemObj.transform.position = direction;

                _items.Add(itemObj);
            }
    }

    private void ActivateShopForge(bool active)
    {
        _shopForge.SetActive(active);

        for (int i = 0; i < _maxCellsShop + Mathf.Clamp(LevelHouse, 0, 4); i++)
            _cellsBuyObjects[i].SetActive(active);
    }

    protected override void OnEnterObject(Collider2D other)
    {
        Player player = other.gameObject.GetComponent<Player>();

        if (player != null)
            ActivateShopForge(true);
    }

    public Item DeleteItem()
    {
        Item item = _items[_items.Count - 1];

        _items.RemoveAt(_items.Count - 1);

        return item;
    }

    protected override void OnExitObject(Collider2D other)
    {
        Player player = other.gameObject.GetComponent<Player>();

        if (player != null)
            ActivateShopForge(false);
    }
}
