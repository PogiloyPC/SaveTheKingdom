using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IEnemyHealth, IHitUnit, IAnimation, IMyPos
{
    [SerializeField] private Animator _anim;

    private StateMachineEnemy _stateMachineEnemy = new StateMachineEnemy();

    private ReturnState _returnState;
    private MovingState _movingState;
    private AttackState _attackState;

    [SerializeField] private LayerMask _targetAttack;

    protected internal LayerMask TargetAttack => _targetAttack;

    [SerializeField] private float _health;
    [SerializeField] private float _damage;
    [SerializeField] private float _speed;
    [SerializeField] private float _distanceAttack;
    [SerializeField] private float _radiusCircleForAttack;
    public float Damage { get { return _damage; } private set { } }
    public float Speed { get { return _speed; } private set { } }
    public float DistanceAttack { get { return _distanceAttack; } private set { } }
    public float RadiusCircleForAttack { get { return _radiusCircleForAttack; } private set { } }



    private void Start()
    {
        StartEnemy();

        _stateMachineEnemy.InitState(_movingState);
    }

    protected abstract void StartEnemy();

    public void TakeDamage(IHitEnemy hit)
    {
        _health -= hit.Hit();

        if (_health <= 0f)
            Death();
    }

    private void Death()
    {
        Destroy(gameObject);
    }

    public float Hit() => _damage;

    public Transform MyPos() => transform;

    public Vector3 PosTarget() => transform.position;

    public void AnimationRun(bool moving) => _anim.SetBool("run", moving);

    public void AnimationAttack() => _anim.SetTrigger("attack");

    protected void InitStates(ReturnState returnState, MovingState movingState, AttackState attackState)
    {
        _returnState = returnState;
        _movingState = movingState;
        _attackState = attackState;
    }

    public StateMachineEnemy GetStateMachine() => _stateMachineEnemy;

    public ReturnState GetReturnState() => _returnState;

    public MovingState GetMovingState() => _movingState;

    public AttackState GetAttackState() => _attackState;

    public bool TargetFixed() => Physics2D.OverlapCircle(transform.position, _radiusCircleForAttack, _targetAttack);

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _radiusCircleForAttack);
    }
}

public interface IMyPos
{
    public Transform MyPos();
}

public interface IAnimation
{
    public void AnimationRun(bool moving);

    public void AnimationAttack();
}

public interface IEnemyHealth : IPosTarget
{
    public void TakeDamage(IHitEnemy hit);
}

public interface IUnitHealth : IPosTarget
{
    public void TakeDamage(IHitUnit hit);
}

public interface IPosTarget
{
    public Vector3 PosTarget();
}

public interface IHitUnit
{
    public float Hit();
}

public interface IHitEnemy
{
    public float Hit();
}