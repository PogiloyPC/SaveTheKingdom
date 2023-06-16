using UnityEngine;

public class Money : MonoBehaviour
{
    [SerializeField] private int _countMoney = 1;

    public int CountMoney { get { return _countMoney; } private set { } }
}
