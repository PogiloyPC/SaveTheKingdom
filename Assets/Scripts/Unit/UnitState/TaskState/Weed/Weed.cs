using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weed : TaskState
{
    private Farmer _unit;

    private Animator _anim;

    private Vector3 _posField => _unit.PosWork;

    private bool _isWorked;

    public Weed(Farmer unit, Animator anim)
    {
        _unit = unit;

        _anim = anim;        
    }

    public override void EnterState()
    {
        if (_unit.transform.position.x > _posField.x)
            _unit.transform.localScale = new Vector3(-1f, 1f, 1f);
        else
            _unit.transform.localScale = new Vector3(1f, 1f, 1f);

        _anim.SetBool("run", true);
    }

    public override void ExitState()
    {
        _anim.SetBool("work", false);

        _isWorked = false;
    }

    public override void PlayState()
    {
        if (_unit.transform.position.x != _posField.x)
        {
            _unit.transform.position = Vector3.MoveTowards(_unit.transform.position, 
                new Vector3(_posField.x, _unit.transform.position.y, 0f), _unit.Speed * Time.deltaTime);
        }
        else if(!_isWorked)
        {
            _isWorked = true;

            _anim.SetBool("run", false);
            _anim.SetBool("work", true);
        }            
    }
}
