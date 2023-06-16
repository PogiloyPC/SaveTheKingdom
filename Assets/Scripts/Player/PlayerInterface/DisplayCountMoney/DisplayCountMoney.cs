using UnityEngine;
using UnityEngine.UI;

public class DisplayCountMoney : MonoBehaviour
{    
    [SerializeField] private Text _countMoney;   

    public void ViewCountMoney(float countMoney)
    {
        _countMoney.text = countMoney.ToString();
    }
}
