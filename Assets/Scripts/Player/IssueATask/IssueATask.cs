using UnityEngine;
using PlayerModification;
using InterfaceTask;

public class IssueATask : IMarkATask
{
    private Transform _posCircle;
    private Transform _posContainerMarks;

    private Mark _mark;

    private PoolObjects<Mark> _poolMarks;

    private ITaskLabel _task;

    public IBuyer _player { get; private set; }

    private LayerMask _taskLayer;

    private float _radiusCircle;

    private int _maxCountMarks = 10;

    private bool _createInStart = true;
    private bool _isAutomatic;

    public IssueATask(Transform posCircle, Transform posContainerMarks, float radiusCircle, LayerMask taskLayer, IBuyer player, Mark mark)
    {
        _posCircle = posCircle;
        _posContainerMarks = posContainerMarks;

        _radiusCircle = radiusCircle;

        _taskLayer = taskLayer;

        _player = player;

        _mark = mark;

        _poolMarks = new PoolObjects<Mark>(_posContainerMarks, _isAutomatic, _mark, _createInStart, _maxCountMarks);
    }

    public void TaskSearched()
    {
        Collider2D collide = Physics2D.OverlapCircle(_posCircle.position, _radiusCircle, _taskLayer);

        ITaskLabel task = collide?.gameObject.GetComponent<TaskCountry>();

        if (task != null)
        {
            if (task != _task)
            {
                if (_task != null)
                    _task.DeselectTask();

                _task = task;

                _task.SelectTask();
            }
        }
        else
        {
            if (_task != null)
            {
                _task.DeselectTask();

                _task = null;
            }
        }
    }

    public ITaskLabel LookForTaks()
    {
        if (_task != null)
        {
            if (_poolMarks.CountObject() > 0)
            {
                if (_player.WantPay().Pay(_task.PriceTask()))
                {
                    Mark mark = _poolMarks.PullOutPool();

                    mark.AttachMark(_task);

                    _task.GetMark(mark);
                    _task.MarkedTask(this);
                    _task.DeselectTask();

                    return _task;
                }
            }
        }

        return null;
    }

    public bool MarkTask() => true;
}
