using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingState : EnemyState
{
    private IMyPos _pos;

    private IAnimation _anim;

    private Vector3 _posMoving;

    private float _speed;

    public MovingState(float speed, IAnimation anim, IMyPos pos)
    {
        _pos = pos;

        _anim = anim;        

        _speed = speed;
    }

    public override void EnterState()
    {
        _anim.AnimationRun(true);
    }

    public override void ExitState()
    {
        _anim.AnimationRun(false);
    }

    public override void PlayState()
    {
        LookObject();

        _pos.MyPos().position = Vector3.MoveTowards(_pos.MyPos().position, new Vector3(_posMoving.x, _pos.MyPos().position.y,
            _pos.MyPos().position.z), _speed * Time.deltaTime);
    }

    public void CheckMovePosition(Vector3 pos) => _posMoving = pos;

    private void LookObject()
    {
        if (_pos.MyPos().position.x > _posMoving.x)
            _pos.MyPos().localScale = new Vector3(-1f, 1f, 1f);
        else
            _pos.MyPos().localScale = new Vector3(1f, 1f, 1f);
    }

}
