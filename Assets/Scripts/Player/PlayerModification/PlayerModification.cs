using PlayerModification.Wallet;
using PoolInterface;
using StructHouse;

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
            public void GetMoney(IHaveMoney money);
        }         
    }
}

namespace PoolInterface
{
    public interface IHaveMoney
    {
        public int GiveMoney();
    }

    public interface IReturnable<T>
    {
        public void GetT(IReturn<T> pool);
    }
}
