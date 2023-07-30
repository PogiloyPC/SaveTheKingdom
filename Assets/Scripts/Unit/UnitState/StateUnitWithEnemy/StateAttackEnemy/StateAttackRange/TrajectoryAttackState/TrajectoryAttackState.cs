using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryAttackState : StateAttackRange
{
    private Bullet _bullet;

    public TrajectoryAttackState(Transform transformUnit, Animator anim, LayerMask enemyLayer, float speed, float radiusCircle, float distance) :
        base(transformUnit, anim, enemyLayer, speed, radiusCircle, distance)
    {

    }

    protected override void Attack()
    {
        AnimationAttack();

        UpdateTimer();
    }
}

