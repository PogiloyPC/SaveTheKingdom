using UnityEngine;

public abstract class Unit : MonoBehaviour
{   
    [SerializeField] private Animator _anim;       

    [SerializeField, Range(0f, 3f)] private float _speed;
    
    public float Speed { get { return _speed; } private set { } }

    private StateMachineUnit _stateMachine;

    private TaskState _taskState;
    private MoveState _moveState;

    private void Start()
    {
        StartUnit();

        _stateMachine = new StateMachineUnit();        
        _stateMachine.InitState(_moveState);
    }

    protected virtual void StartUnit()
    {
    }
    
    protected TaskState GetTaskState()
    {
        return _taskState;
    }
    
    protected MoveState GetMoveState()
    {
        return _moveState;
    }
    
    protected StateMachineUnit GetStateMachineUnit()
    {
        return _stateMachine;
    }
    
    protected void InitState(TaskState taskState, MoveState moveState)
    {
        _taskState = taskState;
        _moveState = moveState;
    }

    public abstract Vector3 LeftBorders();

    public abstract Vector3 RightBorders();    
}
