using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IssueATask : MonoBehaviour
{
    private Transform _posCircle;

    private float _radiusCircle;

    private LayerMask _taskLayer;

    public IssueATask(Transform posCircle, float radiusCircle, LayerMask taskLayer)
    {
        _posCircle = posCircle;

        _radiusCircle = radiusCircle;

        _taskLayer = taskLayer;
    }

    public TaskCountry LookForTaks()
    {
        Collider2D collide = Physics2D.OverlapCircle(_posCircle.position, _radiusCircle, _taskLayer);
        
        TaskCountry task = collide?.gameObject.GetComponent<TaskCountry>();

        if (task != null)
        {
            return task;
        }

        return null;
    }
}
