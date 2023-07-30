using UnityEngine;
using UnitStruct;
using InterfaceTask;

public abstract class UnitCitizenWarrion : UnitCitizen, IWarrion
{
    private Vector3 _postPosition;

    [SerializeField] private float _distanceAttack;
    public float DistanceAttack { get { return _distanceAttack; } private set { } }

    private void Update()
    {
        CheckState();

        GetStateMachineUnit().Update();
    }

    private void CheckState()
    {
        if (CheckEnemy())
        {
            GetStateMachineUnit().ChangeState(GetStateUnitWithEnemy());
        }
        else if (Countr.CurrentDayTime > 0.6f)
        {
            GetStateMachineUnit().ChangeState(GetTaskState());
        }
        else
        {
            GetStateMachineUnit().ChangeState(GetMoveState());
        }
    }

    public void GetPosPost(IGeneratorPosPost generator) => _postPosition = generator.GeneratePosPost();

    public Vector3 PosPost()
    {
        if (Id % 2 == 0)
            return LeftBorders() + _postPosition;
        else
            return RightBorders() + _postPosition;
    }

    public Vector3 MyPos() => transform.position;
}
