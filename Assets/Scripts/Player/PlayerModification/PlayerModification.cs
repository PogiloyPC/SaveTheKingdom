using PlayerModification.Wallet;

namespace PlayerModification
{
    public interface IBuyer
    {
        public IWantPay WantPay();

        public int MoneyCount();
    }

    public interface IMarkATask
    {
        public bool MarkTask();
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
