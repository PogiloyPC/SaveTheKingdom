using UnityEngine;

public class PlayerAnimation
{
    private Animator _player;

    private Transform _playerTransform;

    public PlayerAnimation(Animator player, Transform playerTransform)
    {
        _player = player;

        _playerTransform = playerTransform;
    }

    public void PlayAnimation(float x, float y, PlayerState state)
    {
        if (x != 0 && state.IsRunning() && state.IsGrounded())
        {
            _player.SetBool("run", true);
            if (x >= 0.01f)
                _playerTransform.localScale = new Vector3(1f, 1f, 1f);
            else if (x <= -0.01f)
                _playerTransform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else
        {
            _player.SetBool("run", false);
        }

        if (y >= 0.01f && !state.IsGrounded())
        {
            _player.SetBool("jump", true);
            _player.SetBool("fall", false);
        }
        else if (y <= -0.01f && !state.IsGrounded())
        {
            _player.SetBool("jump", false);
            _player.SetBool("fall", true);
        }
        else
        {
            _player.SetBool("jump", false);
            _player.SetBool("fall", false);
        }
    }

    public void AnimAttack(bool attack)
    {
        if (attack)
            _player.SetTrigger("attack");
    }
}
