using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : EnemyState
{
    private IAnimation _anim;

    private IMyPos _pos;

    private IHitUnit _hitUnit;

    private IUnitHealth _healthUnit;

    private LayerMask _layerTarget;

    private float _speed;
    private float _radiusCircle;
    private float _distanceAttack;
    private float _currentTimerAttack;
    private float _finishTimerAttack = 0.7f;

    public AttackState(IAnimation anim, IMyPos pos, IHitUnit hitUnit, LayerMask layerTarget, float radiusCircle, float distanceAttack, float speed)
    {
        _anim = anim;

        _pos = pos;

        _hitUnit = hitUnit;

        _layerTarget = layerTarget;

        _radiusCircle = radiusCircle;

        _distanceAttack = distanceAttack;

        _speed = speed;
    }

    public override void EnterState()
    {
        _anim.AnimationRun(false);
    }

    public override void ExitState()
    {

    }

    public override void PlayState()
    {
        LookTarget();

        LookRotationTarget();

        CheckTimerAttack();

        if (Vector3.Distance(_healthUnit.PosTarget(), _pos.MyPos().position) <= _distanceAttack)
        {
            if (_currentTimerAttack >= _finishTimerAttack)
                Attack();
        }
        else
        {            
            _pos.MyPos().position = Vector2.MoveTowards(_pos.MyPos().position, new Vector2(_healthUnit.PosTarget().x, _pos.MyPos().position.y)
                , _speed * Time.deltaTime);
        }

    }

    private void LookRotationTarget()
    {
        if (_healthUnit?.PosTarget().x > _pos.MyPos().position.x)
            _pos.MyPos().localScale = new Vector3(1f, 1f, 1f);
        else
            _pos.MyPos().localScale = new Vector3(-1f, 1f, 1f);
    }

    private void Attack()
    {
        _anim.AnimationAttack();

        _healthUnit.TakeDamage(_hitUnit);

        UpdateTimerAttack();
    }

    private void CheckTimerAttack() => _currentTimerAttack += Time.deltaTime;


    private void UpdateTimerAttack() => _currentTimerAttack -= _currentTimerAttack;

    public void LookTarget()
    {
        Collider2D collider = Physics2D.OverlapCircle(_pos.MyPos().position, _radiusCircle, _layerTarget);

        _healthUnit = collider.gameObject.GetComponent<IUnitHealth>();
    }
}
