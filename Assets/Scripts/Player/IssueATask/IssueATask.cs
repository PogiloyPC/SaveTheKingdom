using UnityEngine;
using PlayerModification;
using InterfaceTask;

public class IssueATask : IMarkATask
{
    private Transform _posCircle;

    private ITaskLabel _task;

    private LayerMask _taskLayer;

    private float _radiusCircle;

    public IssueATask(Transform posCircle, float radiusCircle, LayerMask taskLayer)
    {
        _posCircle = posCircle;

        _radiusCircle = radiusCircle;

        _taskLayer = taskLayer;        
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
            _task.MarkedTask(this);
            _task.DeselectTask();

            return _task;
        }

        return null;
    }

    public bool MarkTask()
    {
        return true;
    }
}
