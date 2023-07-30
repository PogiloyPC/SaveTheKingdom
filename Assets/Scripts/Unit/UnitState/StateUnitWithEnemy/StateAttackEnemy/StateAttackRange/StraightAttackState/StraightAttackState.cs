using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightAttackState : StateAttackRange, IDirectionShot
{
    private Bullet _bullet;

    private Vector3 _directionShot;

    public StraightAttackState(Transform transformUnit, Animator anim, LayerMask enemyLayer, float speed,
        float radiusCircle, float distance, Bullet bullet) :
        base(transformUnit, anim, enemyLayer, speed, radiusCircle, distance)
    {
        _bullet = bullet;
    }

    protected override void Attack()
    {
        CheckSideEnemy();

        Bullet bullet = GameObject.Instantiate(_bullet, PosUnit().position, Quaternion.identity);

        bullet.Shot(this);

        AnimationAttack();

        UpdateTimer();
    }

    private void CheckSideEnemy()
    {
        if (EnemyPos.x > PosUnit().position.x)
            _directionShot = Vector3.right;
        else
            _directionShot = Vector3.left;
    }

    public Vector3 DirectionShot() => _directionShot;
}

public interface IDirectionShot
{
    public Vector3 DirectionShot();
}