using UnityEngine;
using UnitStruct;

public class CompleteTaskCountry : TaskState
{
    private Transform _transformUnit;

    private Animator _anim;    

    private Vector3 _posTask;

    private bool _isWorked;

    private float _speed;

    public CompleteTaskCountry(Animator anim, Transform transform, float speed)
    {       
        _anim = anim;

        _transformUnit = transform;

        _speed = speed;
    }

    public override void EnterState()
    {
        if (_transformUnit.position.x > _posTask.x)
            _transformUnit.localScale = new Vector3(-1f, 1f, 1f);
        else
            _transformUnit.localScale = new Vector3(1f, 1f, 1f);

        _anim.SetBool("run", true);
    }

    public override void ExitState()
    {
        _anim.SetBool("work", false);
        
        _isWorked = false;
    }

    public override void PlayState()
    {
        if (_transformUnit.position.x != _posTask.x)
        {
            _transformUnit.position = Vector3.MoveTowards(_transformUnit.position,
                new Vector3(_posTask.x, _transformUnit.position.y, 0f), _speed * Time.deltaTime);            
        }
        else if (!_isWorked)
        {
            _isWorked = true;

            _anim.SetBool("run", false);
            _anim.SetBool("work", true);
        }
    }

    public void LookTaskPos(Vector3 pos)
    {
        _posTask = pos;
    }
}
