using UnityEngine;

public class PlayerMoving
{
    private Rigidbody2D _rb;

    private float _speed;
    private float _forceJump;

    public PlayerMoving(Rigidbody2D rb, float speed, float forceJump)
    {
        _rb = rb;

        _speed = speed;
        _forceJump = forceJump;
    }

    public float Run()
    {       
        if (Input.GetKey(KeyCode.D))
            _rb.velocity = new Vector2(_speed, _rb.velocity.y);
        if (Input.GetKey(KeyCode.A))
            _rb.velocity = new Vector2(-_speed, _rb.velocity.y);

        return _rb.velocity.x;
    }

    public float Jump(bool isJumping)
    {
        if (Input.GetKeyDown(KeyCode.W) && isJumping)
            _rb.AddForce(Vector2.up * _forceJump, ForceMode2D.Impulse);

        return _rb.velocity.y;
    }

    public bool Attack()
    {
        return Input.GetKeyDown(KeyCode.Mouse0);
    }
}
