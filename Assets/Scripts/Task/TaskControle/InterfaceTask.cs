using PlayerModification;
using UnitStruct;
using UnityEngine;

namespace InterfaceTask
{
    public interface ITask
    {
        public void CompleteTheTask(ITasker unit);

        public Vector3 MyPos();        
    }  
    
    public interface ITaskLabel : ITask
    {
        public void MarkedTask(IMarkATask markTask);
        public void SelectTask();
        public void DeselectTask();
    }

    public interface ISurroinding
    {

    }
}
