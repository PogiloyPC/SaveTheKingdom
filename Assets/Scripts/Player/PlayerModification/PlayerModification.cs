using UnityEngine;

namespace PlayerModification
{

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
