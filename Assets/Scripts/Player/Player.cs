using UnityEngine;
using UnityEngine.Events;
using PlayerModification.Wallet;

public class Player : MonoBehaviour
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

    public PlayerWallet Wallet { get; private set; }
    private PlayerMoving _moving;
    private PlayerAnimation _animation;
    private PlayerState _state;

    [SerializeField] private Money _moneyChit;

    private void Start()
    {
        Wallet = new PlayerWallet(_onChangeMoney);
        _animation = new PlayerAnimation(_anim, transform);
        _moving = new PlayerMoving(_rb, _speed, _forceJump);
        _state = new PlayerState(_posGroundPoint, _radiusCircle, _groundLayer);
    }

    private void Update()
    {
        _animation.PlayAnimation(_moving.Run(), _moving.Jump(_state.IsGrounded()), _state);
        _animation.AnimAttack(_moving.Attack());

        if (Input.GetKeyDown(KeyCode.F))
            _buyUnit.DropMoney(Wallet);

        if (Input.GetKeyDown(KeyCode.M))
            Wallet.GetMoney(_moneyChit);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //Money money = other.collider.gameObject.GetComponent<Money>();
        MoneyPlayer moneyPlayer = other.collider.gameObject.GetComponent<MoneyPlayer>();

        //if (money != null)
        //{
        //    Wallet.GetMoney(money);

        //    money.gameObject.SetActive(false);
        //}
        if (moneyPlayer != null)
        {
            Wallet.GetMoney(moneyPlayer);

            moneyPlayer.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        ControleHouse creater = other.gameObject.GetComponent<ControleHouse>();

        if (creater != null) { }


    }

    private void OnTriggerExit2D(Collider2D other)
    {
        ControleHouse creater = other.gameObject.GetComponent<ControleHouse>();

        if (creater != null) { }

    }
}
