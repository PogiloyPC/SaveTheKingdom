using UnityEngine;
using PlayerModification.Wallet;

namespace PlayerModification
{
    public interface IBuyer
    {
        public IWantPay WantPay();

        public int MoneyCount();
    }

    namespace Wallet
    {
        public interface IWantPay
        {
            public bool Pay(int price);
        }

        public interface IGetMoney
        {
            public void GetMoney(Money money);
        }         
    }
}
