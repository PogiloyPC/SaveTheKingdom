using UnityEngine;
using StructHouse;
using InterfaceTask;

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

    public interface IWarrion
    {
        public void GetPosPost(IGeneratorPosPost generator);

        public Vector3 PosPost();

        public Vector3 MyPos();
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
