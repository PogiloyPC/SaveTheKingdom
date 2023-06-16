using System.Collections.Generic;
using UnityEngine;

public class BuyUnit : MonoBehaviour
{
    [SerializeField] private MoneyPlayer _money;

    [SerializeField] private List<MoneyPlayer> _moneys;

    [SerializeField] private float _forceDropMoney;

    [SerializeField] private Transform _posDropMoney;
    [SerializeField] private Transform _container;

    [SerializeField] private bool _isAoutomatic;

    private PoolObjects<MoneyPlayer> _poolMoney;

    private void Start()
    {
        _poolMoney = new PoolObjects<MoneyPlayer>(_moneys, _container, _isAoutomatic, _money);
    }

    public void DropMoney(PlayerWallet wallet)
    {
        if (wallet.Pay(1))
        {
            MoneyPlayer money = _poolMoney.PullOutPool();

            if (money)
            {
                money.transform.position = _posDropMoney.position;
                money.GetComponent<Rigidbody2D>().AddForce(new Vector2(0.5f * transform.localScale.x, 0.5f) * _forceDropMoney,
                    ForceMode2D.Impulse);
            }
        }
    }
}
