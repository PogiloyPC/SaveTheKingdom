using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateAttackUnit : StateUnitWithEnemy
{
    private Animator _anim;

    public float DistanceAttack { get; private set; }
    public float CurrentTimeAttack { get; private set; }
    public abstract float FinishTimeAttack();

    public StateAttackUnit(Transform transformUnit, Animator anim, LayerMask enemyLayer, float speed, float radiusCircle, float distance) :
        base(transformUnit, anim, enemyLayer, speed, radiusCircle)
    {
        _anim = anim;

        DistanceAttack = distance;
    }


    public override void EnterState()
    {
    }

    public override void ExitState()
    {
    }

    public override void PlayState()
    {
        InteractionWithEnemy();
    }

    protected abstract void InteractionWithEnemy();

    protected abstract void Attack();

    protected void Moving(float speed)
    {
        PosUnit().position = Vector2.MoveTowards(PosUnit().position, new Vector2(_enemy.PosTarget().x, PosUnit().position.y), 
        speed * Time.deltaTime);

        AnimationRun(true);
    }        

    public void CheckTimeAttack() => CurrentTimeAttack += Time.deltaTime;

    public void AnimationRun(bool running) => _anim.SetBool("run", running);

    public void AnimationAttack() => _anim.SetTrigger("attack");

    public void UpdateTimer() => CurrentTimeAttack -= CurrentTimeAttack;
}
