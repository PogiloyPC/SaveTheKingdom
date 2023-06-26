using StructHouse;
using InterfaceTask;

namespace CountryModifi
{
    public interface IDistributeTasks
    {
        public void DistributeTasks(ITask task);
    }

    public interface IDeliveryTask
    {
        public void DeliveryTask(ITask task);        
    }    
}
