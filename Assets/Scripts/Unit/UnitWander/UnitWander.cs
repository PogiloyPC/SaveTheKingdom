using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitWander : Unit
{
    [SerializeField] private UnitCitizen[] _unitPatrial;

    [SerializeField] private GameObject _unitTransformationParticle;

    [SerializeField] private LayerMask _moneyMask;

    private Vector3 _startPosWander;

    [SerializeField, Range(0, 2)] private float _maxDistanceWander;
    [SerializeField, Range(0, 5)] private float _radiusCircle;

    protected override void StartUnit()
    {
        _startPosWander = transform.position;

        InitState(new FollowObjectState(this, GetComponent<Animator>()), new MoveState(this, GetComponent<Animator>()),
            new EscapeState(transform, GetComponent<Animator>(), LayerEnemy, Speed, RadiusCircleEnemy));
    }

    private void Update()
    {
        CheckState();

        GetStateMachineUnit().Update();
    }

    private void CheckState()
    {
        Collider2D collide = Physics2D.OverlapCircle(transform.position, _radiusCircle, _moneyMask);

        MoneyPlayer money = collide?.gameObject.GetComponent<MoneyPlayer>();

        if (CheckEnemy() || GetStateUnitWithEnemy().Aghast())
        {
            GetStateMachineUnit().ChangeState(GetStateUnitWithEnemy());
        }
        else if (money != null)
        {
            if (GetTaskState() is FollowObjectState followObject)
            {
                followObject.LookObject(money.MyPos());

                GetStateMachineUnit().ChangeState(GetTaskState());
            }
        }
        else
        {
            GetStateMachineUnit().ChangeState(GetMoveState());
        }
    }

    public override Vector3 LeftBorders()
    {
        return new Vector3(_startPosWander.x - _maxDistanceWander, 0, 0);
    }

    public override Vector3 RightBorders()
    {
        return new Vector3(_startPosWander.x + _maxDistanceWander, 0, 0);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        MoneyPlayer money = other.collider.gameObject.GetComponent<MoneyPlayer>();

        if (money != null)
        {
            money.gameObject.SetActive(false);

            UnitCitizen unitPatrial = Instantiate(BecomePatrial(), transform.position, Quaternion.identity);

            GameObject particle = Instantiate(_unitTransformationParticle);

            particle.transform.SetParent(unitPatrial.transform, false);

            particle.transform.position = unitPatrial.transform.position;

            Destroy(gameObject);
        }
    }

    private UnitCitizen BecomePatrial()
    {
        int number;

        number = Random.Range(0, 5);

        Debug.Log(number);

        return _unitPatrial[number];
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _radiusCircle);
    }
}
