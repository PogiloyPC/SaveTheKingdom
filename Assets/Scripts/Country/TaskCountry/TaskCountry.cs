using UnityEngine;
using UnitStruct;

public abstract class TaskCountry : MonoBehaviour
{
    public abstract void CompleteTheTask(ITasker unit);

    public TaskCountry GetTask() => this;
}
