using System.Collections.Generic;
using UnityEngine;
using StructHouse;
using CountryModifi;

public class Country : MonoBehaviour, IGetHouseFields, IDistributeTasks
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

    private FieldsControle<Farmer, Farm> _farmsControle = new FieldsControle<Farmer, Farm>();
    private FieldsControle<Fisher, Lake> _lakesControle = new FieldsControle<Fisher, Lake>();

    private TaskControle<Bricklayer, StoneMining> _stoneTaskControle = new TaskControle<Bricklayer, StoneMining>();
    private TaskControle<Lumberman, Cutting> _woodTaskControle = new TaskControle<Lumberman, Cutting>();
    private TaskControle<Carpenter, ControleHouse> _buildTaskControle = new TaskControle<Carpenter, ControleHouse>();

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

            CheckUnitProfession(unit);            

            Destroy(posUnit.gameObject);
            Destroy(item.gameObject);
        }
    }

    private void CheckUnitProfession(UnitCitizen unit)
    {
        switch (unit)
        {
            case Farmer:
                Farmer farmer = (Farmer)unit;

                farmer.GetFarmSystem(_farmsControle);

                _farmsControle.GetUnit(farmer);
                break;
            case Fisher:
                Fisher fisher = (Fisher)unit;

                fisher.GetLakeSystem(_lakesControle);

                _lakesControle.GetUnit(fisher);
                break;
            case Bricklayer:
                Bricklayer bricklayer = (Bricklayer)unit;

                bricklayer.GetTaskControle(_stoneTaskControle);

                _stoneTaskControle.GetUnit(bricklayer);
                break;
            case Lumberman:
                Lumberman lumberman = (Lumberman)unit;

                lumberman.GetTaskControle(_woodTaskControle);

                _woodTaskControle.GetUnit(lumberman);
                break;
            case Carpenter:
                Carpenter carpenter = (Carpenter)unit;

                carpenter.GetTaskControle(_buildTaskControle);

                _buildTaskControle.GetUnit(carpenter);
                break;
            default:
                break;
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
            case TypeItem.hamer:
                unitPatrial = _eighthProfession;
                break;
            default:
                break;
        }

        return unitPatrial;
    }

    public void RemoveFreeUnits()
    {

    }

    public void DistributeTasks(TaskCountry task)
    {
        switch (task)
        {
            case StoneMining:
                _stoneTaskControle.GetTasks((StoneMining)task);                
                break;
            case Cutting:
                _woodTaskControle.GetTasks((Cutting)task);                
                break;
            case ControleHouse:
                _buildTaskControle.GetTasks((ControleHouse)task);
                break;
            default:
                break;
        }
    }

    public void GetFields(IHaveField field)
    {
        switch (field)
        {
            case Farm:
                _farmsControle.GetFields((Farm)field);
                break;
            case Lake:
                _lakesControle.GetFields((Lake)field);
                break;
            default:
                break;
        }
    }
}
