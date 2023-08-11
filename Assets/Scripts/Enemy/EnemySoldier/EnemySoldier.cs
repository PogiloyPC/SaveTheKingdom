using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoldier : Enemy
{
    [SerializeField] private DaySystem _daySystem;

    [SerializeField] private Transform _posTownHall;

    private Vector3 _startPos;

    protected override void StartEnemy()
    {
        InitStates(new ReturnState(), new MovingState(Speed, this, this),
            new AttackState(this, this, this, TargetAttack, RadiusCircleForAttack, DistanceAttack, Speed));

        _startPos = transform.position;
    }

    private void Update()
    {
        if (_daySystem.CurrentDayTime < 0.6f)
        {
            GetStateMachine().ChangeState(GetMovingState());

            GetMovingState().CheckMovePosition(_startPos);
        }
        else if (TargetFixed())
        {
            GetStateMachine().ChangeState(GetAttackState());
        }
        else
        {
            GetStateMachine().ChangeState(GetMovingState());

            GetMovingState().CheckMovePosition(_posTownHall.position);
        }

        GetStateMachine().UpdateState();
    }
}
