using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StructHouse
{
    public interface IHaveField
    {
        public int CountFields();

        public List<Vector3> Fields();
    }   

    public interface IReturn<T>
    {
        public void Return(T t);
    }
    
    public interface IReturnUnit<T> : IReturn<T>
    {
        public void ReturnUnit(T t);
    }

    public interface IActiveObject
    {
        public void SetActive(IChangeActive changeActive);
    }

    public interface IChangeActive
    {
        public bool OnSwitchActive();
    }    
}
