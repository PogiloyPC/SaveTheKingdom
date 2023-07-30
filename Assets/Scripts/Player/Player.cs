using UnityEngine;
using UnityEngine.Events;
using SystemObject;
using PlayerModification;
using PlayerModification.Wallet;
using CountryModifi;

public class Player : MonoBehaviour, IBuyer, IChangeActiveObject
{
    [SerializeField] private Animator _anim;

    [SerializeField] private Rigidbody2D _rb;

    [SerializeField] private Transform _posGroundPoint;

    [SerializeField] private UnityEvent<float> _onChangeMoney;

    [SerializeField] private BuyUnit _buyUnit;

    [SerializeField] private LayerMask _groundLayer;

    [SerializeField] private float _speed;
    [SerializeField] private float _forceJump;
    [SerializeField] private float _radiusCircle;

    private PlayerWallet _wallet;
    private PlayerMoving _moving;
    private PlayerAnimation _animation;
    private PlayerState _state;

    [Header("Task")]
    [SerializeField] private Transform _posCircleTask;
    [SerializeField] private Transform _posContainer;

    [SerializeField] private Mark _mark;

    private IssueATask _issueATask;

    [SerializeField] private float _radiuseCircleTask;

    [SerializeField] private IDeliveryTask _deliveriTask;

    [SerializeField] private LayerMask _taskMask;

    [SerializeField] private MoneyPlayer _moneyChit;

    private void Start()
    {
        _wallet = new PlayerWallet(_onChangeMoney);
        _animation = new PlayerAnimation(_anim, transform);
        _moving = new PlayerMoving(_rb, _speed, _forceJump);
        _state = new PlayerState(_posGroundPoint, _radiusCircle, _groundLayer);

        _deliveriTask = GameObject.Find("Country").GetComponent<Country>();

        _issueATask = new IssueATask(_posCircleTask, _posContainer, _radiuseCircleTask, _taskMask, this, _mark);        
    }

    private void Update()
    {
        _animation.PlayAnimation(_moving.Run(), _moving.Jump(_state.IsGrounded()), _state);
        _animation.AnimAttack(_moving.Attack());

        if (Input.GetKeyDown(KeyCode.F))
            _buyUnit.DropMoney(_wallet);

        if (Input.GetKeyDown(KeyCode.M))
            _wallet.GetMoney(_moneyChit);

        _issueATask.TaskSearched();

        if (Input.GetKeyDown(KeyCode.X))
            FindTask();
    }

    private void FindTask() => _deliveriTask.DeliveryTask(_issueATask.LookForTaks());

    public IWantPay WantPay() => _wallet;

    public int MoneyCount() => _wallet.MoneyCount;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(_posCircleTask.position, _radiuseCircleTask);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        MoneyPlayer moneyPlayer = other.collider.gameObject.GetComponent<MoneyPlayer>();

        if (moneyPlayer != null)
        {
            _wallet.GetMoney(moneyPlayer);

            moneyPlayer.gameObject.SetActive(false);
        }
    }

    public bool SetTrue() => true;    

    public bool SetFalse() => false;    
}
