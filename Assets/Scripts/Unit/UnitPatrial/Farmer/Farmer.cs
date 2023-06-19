using UnityEngine;

public class Farmer : UnitCitizen
{
    [SerializeField] private Vector3 _posWork;

    public Vector3 PosWork => _posWork;

    [SerializeField] private bool _haveField;

    protected override void StartCitizenUnit()
    {
        InitState(new Weed(this, GetComponent<Animator>()), new MoveState(this, GetComponent<Animator>()));
    }

    private void Update()
    {
        if (Countr.CurrentTime <= 0.6f && _haveField)
            GetStateMachineUnit().ChangeState(GetTaskState());
        else
            GetStateMachineUnit().ChangeState(GetMoveState());

        GetStateMachineUnit().Update();
    }

    public void TakePosField(Vector3 posWork)
    {
        _posWork = posWork;

        _haveField = true;
    }
}
