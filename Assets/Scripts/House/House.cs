using UnityEngine;
using UnityEngine.Events;

public class House : MonoBehaviour
{
    [SerializeField] private string _nameHouse;

    [SerializeField] private int _levelHouse;
    [SerializeField] private int _healthHouse;
    private int _healthUpCount = 2;
    
    
    public string NameHouse { get { return _nameHouse; } private set { } }

    public int LevelHouse { get { return _levelHouse; } private set { } }        
    public int HealthHouse { get { return _healthHouse; } private set { } }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.gameObject.GetComponent<Player>();

        if (player != null)
        {
            
        }

        OnEnterObject(other);
    }

    public void LevelUp()
    {
        _levelHouse++;

        UpgradeHealth();
    }

    private void UpgradeHealth()
    {
        _healthHouse += _healthUpCount;
    }

    private void OnTriggerExit2D(Collider2D other)
    {        
        OnExitObject(other);
    }

    protected virtual void OnEnterObject(Collider2D other)
    {

    }

    protected virtual void OnExitObject(Collider2D other)
    {

    }
}
