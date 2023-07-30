using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateAttackEnemy : StateUnitWithEnemy
{
    private Vector2 _offset;

    public StateAttackEnemy(Transform transformUnit, Animator anim, LayerMask enemyLayer, float speed, float radiusCircle, Vector2 offset) :
        base(transformUnit, anim, enemyLayer, speed, radiusCircle)
    {
        _offset = offset;
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
            

            LookRotationEnemy();
        }
    }
}
