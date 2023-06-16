using UnityEngine;

public abstract class UnitCitizen : Unit
{
    private Country _country;
    protected Country Country => _country;

    [SerializeField] private int _id;

    public int Id { get { return _id; } private set { } }   

    protected override void StartUnit()
    {
        _country = GameObject.Find("Country").GetComponent<Country>();

        _id = _country._generatorId.GenerateNewId();        

        StartCitizenUnit();        

        _country.AddFreeUnits(this);
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
