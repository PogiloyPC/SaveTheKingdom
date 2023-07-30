using PlayerModification.Wallet;
using UnityEngine.Events;
using PoolInterface;

public class PlayerWallet : IWantPay, IGetMoney
{
    private UnityEvent<float> OnChangeMoney;

    private int _moneyCount = 0;          

    public int MoneyCount { get { return _moneyCount; } private set { } }

    public PlayerWallet(UnityEvent<float> onEvent)
    {
        OnChangeMoney = onEvent;
    }

    public bool Pay(int price)
    {
        bool buy;

        if (_moneyCount >= price && price > 0)
        {
            _moneyCount -= price;

            OnChangeMoney?.Invoke(_moneyCount);

            buy = true;
        }
        else
        {
            buy = false;
        }

        return buy;
    }

    public void GetMoney(IHaveMoney money)
    {
        if (money.GiveMoney() <= 0)
            return;

        _moneyCount += money.GiveMoney();

        OnChangeMoney?.Invoke(_moneyCount);
    }
}
