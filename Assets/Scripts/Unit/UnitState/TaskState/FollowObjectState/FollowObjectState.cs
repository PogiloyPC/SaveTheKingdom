using UnityEngine;

public class FollowObjectState : TaskState
{
    private Vector3 _pos;

    private Unit _unit;

    private Animator _anim;

    public FollowObjectState(Unit unit, Animator anim)
    {
        _unit = unit;

        _anim = anim;
    }

    public override void EnterState()
    {
        if (_unit.transform.position.x > _pos.x)
            _unit.transform.localScale = new Vector3(-1f, 1f, 1f);
        else
            _unit.transform.localScale = new Vector3(1f, 1f, 1f);

        _anim.SetBool("run", true);
    }

    public override void PlayState()
    {
        _unit.transform.position = Vector3.MoveTowards(_unit.transform.position, _pos, _unit.Speed * Time.deltaTime);
    }

    public override void ExitState()
    {
        _anim.SetBool("run", false);
    }

    public void LookObject(Vector3 pos)
    {
        _pos = pos;
    }
}
