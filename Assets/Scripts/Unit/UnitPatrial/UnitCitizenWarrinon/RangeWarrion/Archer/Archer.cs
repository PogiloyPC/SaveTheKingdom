using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : UnitCitizenWarrion
{
    protected override void StartCitizenUnit()
    {
        InitState(new GuardState(this, Speed, GetComponent<Animator>(), transform), new MoveState(this, GetComponent<Animator>()),
            new TrajectoryAttackState(transform, GetComponent<Animator>(), LayerEnemy, Speed, RadiusCircleEnemy, DistanceAttack));
    }
}
