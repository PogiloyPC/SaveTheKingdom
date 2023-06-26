using UnityEngine;
using StructHouse;

namespace UnitStruct 
{
    public interface IWorkInField
    {
        public Vector3 PosWork();

        public void TakePosField(Vector3 posWork);
    }

    public interface ICompleteTheTask<T>
    {
        public void TakeTask(T t);

        public T ReturnTask();
    }

    public interface ITasker
    {
        public void FinishedTask();

        public float GetDamage();
    }

    public enum TypeUnitCitizen
    {
        UnitCitizen,
        Bricklayer,
        Carpenter,
        Lumberman,
        Farmer,
        Fisher,
        Swordman,
        Archer,
        Wizard,
        Spearman
    }
}
