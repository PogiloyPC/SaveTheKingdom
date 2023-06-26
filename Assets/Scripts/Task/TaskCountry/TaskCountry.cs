using UnityEngine;
using UnitStruct;
using PlayerModification;
using InterfaceTask;

public abstract class TaskCountry : MonoBehaviour, ITaskLabel, ISurroinding
{
    [SerializeField] private BoxCollider2D _collide;

    [SerializeField] private SpriteRenderer _render;

    public abstract void CompleteTheTask(ITasker unit);

    private bool _isMarked;

    public Vector3 MyPos() => transform.position;

    public void MarkedTask(IMarkATask markTask)
    {
        _isMarked = markTask.MarkTask();

        gameObject.layer = 11;

        //if (_isMarked)
        //    _collide.enabled = false;
    }

    public void SelectTask()
    {
        _render.color = Color.green;
    }

    public void DeselectTask()
    {
        _render.color = Color.white;
    }

}
