using UnityEngine;
using System;
using StructHouse;
using UnitStruct;

public class Fisher : UnitCitizen, IWorkInField
{
    private IReturn<Fisher> _lakeSystem;

    private Action<Fisher> _onDead;

    [SerializeField] private MoneyPlayer _money;

    [SerializeField] private Transform _pos;

    [SerializeField] private bool _haveField;

    private Vector3 _posWork;

    protected override void StartCitizenUnit()
    {
        InitState(new FieldWorkState(this, GetComponent<Animator>(), transform, Speed), new MoveState(this, GetComponent<Animator>()));
    }

    private void Update()
    {
        if (Countr.CurrentDayTime <= 0.6f && _haveField)
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

    private void OnDestroy()
    {
        _onDead.Invoke(this);

        _onDead -= _lakeSystem.Return;
    }

    public void GetLakeSystem(IReturn<Fisher> lakeSystem)
    {
        _lakeSystem = lakeSystem;

        _onDead += _lakeSystem.Return;
    }   

    public void CatchFish()
    {
        Rigidbody2D rb = Instantiate(_money, _pos.position, Quaternion.identity).GetComponent<Rigidbody2D>();

        rb.AddForce(transform.up * 5f, ForceMode2D.Impulse);
    }

    public Vector3 PosWork() => _posWork;
}
