using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateAttackRange : StateAttackUnit
{
    public override float FinishTimeAttack() => 1f;    

    public StateAttackRange(Transform transformUnit, Animator anim, LayerMask enemyLayer, float speed, float radiusCircle, float distance) :
        base(transformUnit, anim, enemyLayer, speed, radiusCircle, distance)
    {

    }

    protected override void InteractionWithEnemy()
    {
        LookEnemy();

        if (EnemyFixed())
        {
            CheckTimeAttack();

            if (Vector2.Distance(PosUnit().position, _enemy.PosTarget()) <= DistanceAttack / 1.5f)
            {
                Moving(-Speed);
            }
            else if (Vector2.Distance(PosUnit().position, _enemy.PosTarget()) <= DistanceAttack)
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
}
