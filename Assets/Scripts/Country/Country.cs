using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Country : MonoBehaviour
{
    [SerializeField] private SizeCountry _sizeCountry;

    [SerializeField] private Forge _forge;

    public GeneratorIdUnit _generatorId;

    [SerializeField] private DaySystem _daySystem;

    [SerializeField] private List<UnitCitizen> _freeUnitsPatrial;
    public IReadOnlyCollection<Item> ItemsInShop => _forge.Items;

    [SerializeField] private Lumberman _firstProfession;
    [SerializeField] private Swordsman _secondProfession;
    [SerializeField] private Archer _thirdProfession;
    [SerializeField] private Bricklayer _fourthProfession;
    [SerializeField] private Farmer _fifthProfession;
    [SerializeField] private Fisher _sixthProfession;
    [SerializeField] private Spearman _seventhProfession;
    [SerializeField] private Carpenter _eighthProfession;
    [SerializeField] private Wizard _ninthProfession;

    private FarmsControle _farmsControle = new FarmsControle();

    public Vector3 RightBorders => _sizeCountry.RightBorders;
    public Vector3 LeftBorders => _sizeCountry.LeftBorders;
    public Vector3 ForgePosition => _forge.transform.position;

    public float CurrentTime => _daySystem.CurrentDayTime;

    public void AddFreeUnits(UnitCitizen unitPatrial)
    {
        bool findUnit = false;

        for (int i = 0; i < _freeUnitsPatrial.Count; i++)
        {           
            if (_freeUnitsPatrial[i].Id != unitPatrial.Id)
            {
                findUnit = true;
            }
            else
            {
                findUnit = false;

                break;
            }
        }

        if (findUnit)
            _freeUnitsPatrial.Add(unitPatrial);
    }

    public void UpgradeUnit(Transform posUnit, UnitCitizen freeUnitPatrial, Item item)
    {
        bool findPatrial = false;

        for (int i = 0; i < _freeUnitsPatrial.Count; i++)
        {
            if (_freeUnitsPatrial[i].Id == freeUnitPatrial.Id)
            {
                findPatrial = true;

                _freeUnitsPatrial.Remove(freeUnitPatrial);

                break;
            }
            else
            {
                findPatrial = false;
            }
        }

        if (findPatrial)
        {
            UnitCitizen unit = Instantiate(ChooseProfession(item), posUnit.position, Quaternion.identity);

            if (unit is Farmer farmer)
                _farmsControle.GetFarmer(farmer);

            Destroy(posUnit.gameObject);
            Destroy(item.gameObject);
        }
    }

    private UnitCitizen ChooseProfession(Item item)
    {
        UnitCitizen unitPatrial = new UnitFreeCitizen();

        switch (item.ItemType)
        {
            case TypeItem.pickaxe:
                unitPatrial = _fourthProfession;
                break;
            case TypeItem.axe:
                unitPatrial = _firstProfession;
                break;
            case TypeItem.bow:
                unitPatrial = _thirdProfession;
                break;
            case TypeItem.fishingRod:
                unitPatrial = _sixthProfession;
                break;
            case TypeItem.hoe:
                unitPatrial = _fifthProfession;               
                break;
            case TypeItem.spear:
                unitPatrial = _seventhProfession;
                break;
            case TypeItem.staff:
                unitPatrial = _ninthProfession;
                break;
            case TypeItem.sword:
                unitPatrial = _secondProfession;
                break;
            default:
                break;
        }               

        return unitPatrial;
    }

    public void RemoveFreeUnits()
    {

    }

    public void GetFarm(Farm farm)
    {
        _farmsControle.GetFields(farm);
    }
}
