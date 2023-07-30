using System.Collections;
using System.Collections.Generic;
using UnitStruct;
using UnityEngine;

public class EscapeState : StateUnitWithEnemy
{
    private Animator _anim;

    public EscapeState(Transform transformUnit, Animator anim, LayerMask enemyLayer, float speed, float radiusCircle) :
        base(transformUnit, anim, enemyLayer, speed, radiusCircle)
    {
        _anim = anim;
    }

    public override void EnterState()
    {
    }

    public override void ExitState()
    {
    }

    public override void PlayState()
    {
        LookEnemy();

        if (EnemyFixed())
        {
            PosUnit().position = Vector2.MoveTowards(PosUnit().position,
                new Vector2(EnemyPos.x, PosUnit().position.y), -Speed * Time.deltaTime);

            TimerAghast = StartTimerAghast;

            LookRotationEnemy();
        }
        else
        {
            TimerAghast -= Time.deltaTime;
        }

        _anim.SetBool("run", Physics2D.OverlapCircle(PosUnit().position, RadiusCircle, EnemyLayer));
    }
}
