using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Country : MonoBehaviour
{
    [SerializeField] private SizeCountry _sizeCountry;

    [SerializeField] private Forge _forge;

    public GeneratorIdUnit _generatorId;

    [SerializeField] private List<UnitCitizen> _freeUnitsPatrial;

    [SerializeField] private Lumberman _firstProfession;
    [SerializeField] private Swordsman _secondProfession;
    [SerializeField] private Archer _thirdProfession;
    [SerializeField] private Bricklayer _fourthProfession;
    //[SerializeField] private Farmer _fifthProfession;
    [SerializeField] private Fisher _sixthProfession;
    [SerializeField] private Spearman _seventhProfession;
    [SerializeField] private Carpenter _eighthProfession;

    private UnitCitizen unitPatrial;

    public IReadOnlyList<Item> ItemsInShop => _forge.Items;

    public Vector3 RightBorders => _sizeCountry.RightBorders;
    public Vector3 LeftBorders => _sizeCountry.LeftBorders;
    public Vector3 ForgePosition => _forge.transform.position;

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
            Instantiate(ChooseProfession(item), posUnit.position, Quaternion.identity);

            Destroy(posUnit.gameObject);
            Destroy(item.gameObject);
        }
    }

    private UnitCitizen ChooseProfession(Item item)
    {
        

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
            //case TypeItem.hoe:
            //    unitPatrial = _fifthProfession;
                //break;
            case TypeItem.spear:
                unitPatrial = _seventhProfession;
                break;
            //case TypeItem.staff:
            //    unitPatrial = _fourthProfession;
            //    break;
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
}
