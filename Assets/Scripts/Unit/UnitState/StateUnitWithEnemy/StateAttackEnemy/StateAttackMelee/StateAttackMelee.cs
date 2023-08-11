using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateAttackMelee : StateAttackUnit
{
    private IHitEnemy _hit;

    public override float FinishTimeAttack() => 0.5f;

    public StateAttackMelee(Transform transformUnit, Animator anim, LayerMask enemyLayer, float speed, float radiusCircle, 
        float distance, IHitEnemy hit) :
        base(transformUnit, anim, enemyLayer, speed, radiusCircle, distance)
    {
        _hit = hit;
    }

    protected override void InteractionWithEnemy()
    {
        LookEnemy();

        if (EnemyFixed())
        {
            CheckTimeAttack();

            if (Vector2.Distance(PosUnit().position, _enemy.PosTarget()) <= DistanceAttack)
            {
                AnimationRun(false);

                if (CurrentTimeAttack >= FinishTimeAttack())
                    Attack();
            }
            else
            {
                Moving(Speed);              
            }

            LookRotationEnemy();
        }
    }

    protected override void Attack()
    {
        AnimationAttack();

        _enemy.TakeDamage(_hit);

        UpdateTimer();
    }
}
