using System.Collections;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    [SerializeField] private Animator _anim;

    private StateMachineUnit _stateMachine;

    private TaskState _taskState;
    private MoveState _moveState;
    private StateUnitWithEnemy _stateUnitWithEnemy;

    [SerializeField] private LayerMask _layerEnemy;
    public LayerMask LayerEnemy { get { return _layerEnemy; } private set { } }
   
    [SerializeField, Range(0f, 3f)] private float _speed;
    [SerializeField] private float _radiusCircleEnemy;
    public float Speed { get { return _speed; } private set { } }
    public float RadiusCircleEnemy { get { return _radiusCircleEnemy; } private set { } }    

    private void Start()
    {       
        StartUnit();

        _stateMachine = new StateMachineUnit();
        _stateMachine.InitState(_moveState);
    }

    protected virtual void StartUnit()
    {
    }

    protected bool CheckEnemy() => Physics2D.OverlapCircle(transform.position, _radiusCircleEnemy, _layerEnemy);

    protected StateUnitWithEnemy GetStateUnitWithEnemy() => _stateUnitWithEnemy;

    protected TaskState GetTaskState() => _taskState;

    protected MoveState GetMoveState() => _moveState;

    protected StateMachineUnit GetStateMachineUnit() => _stateMachine;

    protected void InitState(TaskState taskState, MoveState moveState, StateUnitWithEnemy stateEnemy)
    {
        _taskState = taskState;
        _moveState = moveState;
        _stateUnitWithEnemy = stateEnemy;
    }

    public abstract Vector3 LeftBorders();

    public abstract Vector3 RightBorders();    

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _radiusCircleEnemy);
    }
}
