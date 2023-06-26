using System.Collections.Generic;
using UnityEngine;
using StructHouse;
using CountryModifi;
using InterfaceTask;

public class Country : MonoBehaviour, IDeliveryTask
{
    [SerializeField] private SizeCountry _sizeCountry;

    [SerializeField] private Forge _forge;

    public GeneratorIdUnit _generatorId;

    [SerializeField] private DaySystem _daySystem;

    [SerializeField] private List<UnitCitizen> _freeUnitsPatrial;
    public IReadOnlyCollection<Item> ItemsInShop => _forge.Items;

    [SerializeField] private UnitCitizenTask _bricklayerProfession;
    [SerializeField] private Swordsman _secondProfession;
    [SerializeField] private Archer _thirdProfession;
    [SerializeField] private UnitCitizenTask _lumbermanProfession;
    [SerializeField] private UnitCitizenTask _farmerProfession;
    [SerializeField] private UnitCitizenTask _fisherProfession;
    [SerializeField] private Spearman _seventhProfession;
    [SerializeField] private UnitCitizenTask _carpenterProfession;
    [SerializeField] private Wizard _ninthProfession;    

    private TaskControle _taskControle = new TaskControle();

    public Vector3 RightBorders => _sizeCountry.RightBorders;
    public Vector3 LeftBorders => _sizeCountry.LeftBorders;
    public Vector3 ForgePosition => _forge.transform.position;

    public float CurrentDayTime => _daySystem.CurrentDayTime;

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

            _taskControle.CheckUnitProfession(unit);            

            Destroy(posUnit.gameObject);
            Destroy(item.gameObject);
        }
    }

    private UnitCitizen ChooseProfession(Item item)
    {
        UnitCitizen unitPatrial = null;

        switch (item.ItemType)
        {
            case TypeItem.pickaxe:
                unitPatrial = _bricklayerProfession;
                break;
            case TypeItem.axe:
                unitPatrial = _lumbermanProfession;
                break;
            case TypeItem.bow:
                unitPatrial = _thirdProfession;
                break;
            case TypeItem.fishingRod:
                unitPatrial = _fisherProfession;
                break;
            case TypeItem.hoe:
                unitPatrial = _farmerProfession;
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
            case TypeItem.hamer:
                unitPatrial = _carpenterProfession;
                break;
            default:
                break;
        }

        return unitPatrial;
    }

    public void RemoveFreeUnits()
    {

    }     

    public void DeliveryTask(ITask task)
    {        
        _taskControle.DistributeTasks(task);       
    }   
}
