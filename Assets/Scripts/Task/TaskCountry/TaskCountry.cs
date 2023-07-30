using UnityEngine;
using UnitStruct;
using PlayerModification;
using InterfaceTask;

public abstract class TaskCountry : MonoBehaviour, ITaskLabel, ISurroinding
{
    [SerializeField] private BoxCollider2D _collide;

    [SerializeField] private SpriteRenderer _render;

    [SerializeField] private uint _numberLayer = 11;

    private int _priceTask = 1;

    private Mark _mark;

    public abstract void CompleteTheTask(ITasker unit);

    private bool _isMarked;

    public Vector3 MyPos() => transform.position;

    public void MarkedTask(IMarkATask markTask)
    {
        _isMarked = markTask.MarkTask();

        gameObject.layer = (int)_numberLayer;
    }

    public int PriceTask() => _priceTask;

    public void SelectTask() => _render.color = Color.green;

    public void DeselectTask() => _render.color = Color.white;

    public void GetMark(Mark mark)
    {
        if (!_mark)
            _mark = mark;
    }

    private void OnDestroy()
    {
        _mark?.gameObject.SetActive(false);
    }
}
