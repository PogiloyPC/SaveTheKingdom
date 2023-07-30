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

    public interface IFieldForMillet
    {
        public float CurrentValue();
        public float FinishValue();

        public Vector3 MyPos();
    }
    
    public interface IFieldForFish
    {        
        public float FinishValue();

        public Vector3 MyPos();

        public bool IsPulled();
    }
    
    public interface ITaskLabel : ITask
    {
        public void MarkedTask(IMarkATask markTask);
        public void SelectTask();
        public void DeselectTask();
        public int PriceTask();
        public void GetMark(Mark mark);
    }

    public interface IGeneratorPosPost
    {
        public Vector3 GeneratePosPost();
    }

    public interface ISurroinding
    {
    }    
}

namespace SystemObject
{
    public interface ISetActiveObject
    {
        public void OnEnableObject(IChangeActiveObject active);
        public void OnDisableObject(IChangeActiveObject active);
    }

    public interface IChangeActiveObject
    {
        public bool SetTrue();
        public bool SetFalse();
    }
}
