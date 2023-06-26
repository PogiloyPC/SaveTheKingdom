using UnityEngine;
using UnitStruct;

public abstract class UnitCitizen : Unit
{
    [SerializeField] private TypeUnitCitizen _typeUnit;
    public TypeUnitCitizen TypeUnit { get { return _typeUnit; } private set { } }

    private Country _country;
    protected Country Countr => _country;

    [SerializeField] private int _id;

    public int Id { get { return _id; } private set { } }   

    protected override void StartUnit()
    {
        _country = GameObject.Find("Country").GetComponent<Country>();

        _id = _country._generatorId.GenerateNewId();        

        _country.AddFreeUnits(this);

        StartCitizenUnit();        
    }

    protected abstract void StartCitizenUnit();       
    
    public override Vector3 LeftBorders()
    {
        return _country.LeftBorders;
    }
    
    public override Vector3 RightBorders()
    {
        return _country.RightBorders;
    }
}
