using System.Collections;
using UnityEngine;
using UnitStruct;

public class GuardState : TaskState
{
    private Transform _transform;

    private Animator _anim;

    private IWarrion _warrion;

    private float _speed;

    public GuardState(IWarrion warrion, float speed, Animator anim, Transform transform)
    {
        _transform = transform;

        _anim = anim;

        _warrion = warrion;

        _speed = speed;
    }

    public override void EnterState()
    {
        if (_warrion.PosPost().x > _warrion.MyPos().x)
            _transform.localScale = new Vector3(1f, 1f, 1f);
        else
            _transform.localScale = new Vector3(-1f, 1f, 1f);
    }

    public override void ExitState()
    {

    }

    public override void PlayState()
    {
        if (_warrion.MyPos().x != _warrion.PosPost().x)
        {
            _transform.position = Vector3.MoveTowards(_transform.position,
                new Vector3(_warrion.PosPost().x, _transform.position.y), _speed * Time.deltaTime);

            _anim.SetBool("run", true);
        }
        else
        {
            _anim.SetBool("run", false);
        }
    }
}
