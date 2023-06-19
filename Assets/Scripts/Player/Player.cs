using UnityEngine;
using UnityEngine.Events;
using PlayerModification;
using PlayerModification.Wallet;

public class Player : MonoBehaviour, IBuyer
{
    [SerializeField] private Animator _anim;

    [SerializeField] private Rigidbody2D _rb;

    [SerializeField] private Transform _posGroundPoint;

    [SerializeField] private UnityEvent<float> _onChangeMoney;

    [SerializeField] private float _speed;
    [SerializeField] private float _forceJump;
    [SerializeField] private float _radiusCircle;

    [SerializeField] private LayerMask _groundLayer;

    [SerializeField] private BuyUnit _buyUnit;

    private PlayerWallet _wallet;
    private PlayerMoving _moving;
    private PlayerAnimation _animation;
    private PlayerState _state;

    [SerializeField] private Money _moneyChit;

    private void Start()
    {
        _wallet = new PlayerWallet(_onChangeMoney);
        _animation = new PlayerAnimation(_anim, transform);
        _moving = new PlayerMoving(_rb, _speed, _forceJump);
        _state = new PlayerState(_posGroundPoint, _radiusCircle, _groundLayer);
    }

    private void Update()
    {
        _animation.PlayAnimation(_moving.Run(), _moving.Jump(_state.IsGrounded()), _state);
        _animation.AnimAttack(_moving.Attack());

        if (Input.GetKeyDown(KeyCode.F))
            _buyUnit.DropMoney(_wallet);

        if (Input.GetKeyDown(KeyCode.M))
            _wallet.GetMoney(_moneyChit);
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

    public IWantPay WantPay()
    {
        return _wallet;
    }

    public int MoneyCount()
    {
        return _wallet.MoneyCount;
    }
}
